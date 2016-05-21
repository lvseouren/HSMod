using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private GameManager() { }

    #endregion

    public GameState CurrentGameState;

    public Player EnemyPlayer;
    public Player SelfPlayer;
    public Player CurrentPlayer;

    private Vector3 BOTTOM_CENTER = new Vector3(798f, 60f, 230f);
    private Vector3 BOTTOM_HAND = new Vector3(0f, -11f, 0f);
    private Vector3 BOTTOM_MANA = new Vector3(8.05f, -3.25f, 0f);
    private Vector3 BOTTOM_BOARD = new Vector3(0f, 5.5f, 0f);

    private Vector3 TOP_CENTER = new Vector3(800f, 60f, 935f);
    private Vector3 TOP_HAND = new Vector3(0f, 12f, 0f);
    private Vector3 TOP_MANA = new Vector3(8.05f, 3.25f, 0f);
    private Vector3 TOP_BOARD = new Vector3(0f, -4f, 0f);

    public void Start()
    {
        _instance = this;

        // TODO : Move quality stuff to new class
        QualitySettings.vSyncCount = 1;

        #region Test Zone

        PlayerParameters bottomParameters = new PlayerParameters()
        {
            HeroClass = HeroClass.DeathKnight,
            HeroHealth = 30,
            HeroArmor = 0,

            PlayerPosition = BOTTOM_CENTER,

            HandPosition = BOTTOM_HAND,
            HandInverted = false,

            ManaPosition = BOTTOM_MANA,
            DisplayCrystals = true,

            BoardPosition = BOTTOM_BOARD,

            HeroPower = typeof(RaiseGhoul),

            Deck = new List<BaseCard>()
            {
                new SkeletonCommander(),
                new GoblinMerchant(),
                new IllFatedSquire(),
                new CorpseExplosion(),
                new DancingRuneblade(),
                new DeathwhisperNecrolyte(),
                new ArcaneAnomaly(),
                new TreacherousMercenary(),
                new GoblinMerchant(),
                new IllFatedSquire(),
                new CorpseExplosion(),
                new AllWillServe(),
                new DancingRuneblade(),
                new SkeletonCommander(),
                new DeathwhisperNecrolyte(),
                new AllWillServe(),
                new TreacherousMercenary(),
                new ArcaneAnomaly(),
            },
        };

        SelfPlayer = Player.Create(bottomParameters);

        PlayerParameters topParameters = new PlayerParameters()
        {
            HeroClass = HeroClass.DeathKnight,
            HeroHealth = 30,
            HeroArmor = 0,

            PlayerPosition = TOP_CENTER,

            HandPosition = TOP_HAND,
            HandInverted = true,

            ManaPosition = TOP_MANA,
            DisplayCrystals = false,

            BoardPosition = TOP_BOARD,

            HeroPower = typeof(RaiseGhoul),

            Deck = new List<BaseCard>()
            {
                new ArcaneAnomaly(),
                new SkeletonCommander(),
                new TreacherousMercenary(),
                new GoblinMerchant(),
                new IllFatedSquire(),
                new DeathwhisperNecrolyte(),
                new DancingRuneblade(),
                new SkeletonCommander(),
                new AllWillServe(),
                new DeathwhisperNecrolyte(),
                new TreacherousMercenary(),
                new GoblinMerchant(),
                new CorpseExplosion(),
                new AllWillServe(),
                new DancingRuneblade(),
                new IllFatedSquire(),
                new ArcaneAnomaly(),
                new CorpseExplosion()
            },
        };

        EnemyPlayer = Player.Create(topParameters);

        #endregion

        SelfPlayer.Enemy = EnemyPlayer;
        EnemyPlayer.Enemy = SelfPlayer;

        // Randomize the starting player
        if (Random.Range(0, 2) == 1)
        {
            CurrentPlayer = EnemyPlayer;
        }
        else
        {
            CurrentPlayer = SelfPlayer;
        }

        Mulligan();

        TurnStart();
    }

    public void Mulligan()
    {
        Debugger.Log("Mulligan phase start");
        
        CurrentGameState = GameState.Mulligan;

        // TODO : Rework mulligan

        // TODO: Give coin
        if (CurrentPlayer.Equals(SelfPlayer))
        {
            SelfPlayer.Draw(3);
            EnemyPlayer.Draw(4);
        }
        else
        {
            EnemyPlayer.Draw(3);
            SelfPlayer.Draw(4);
        }

        Debugger.Log("Mulligan phase end");
    }

    public void TurnStart()
    {
        Debugger.Log("Turn start");

        // Switching to Start Turn state
        CurrentGameState = GameState.Start;

        if (CurrentPlayer == SelfPlayer)
        {
            InterfaceManager.Instance.SpawnTurnSprite();
        }

        // Firing OnTurnStart events
        EventManager.Instance.OnTurnStart(CurrentPlayer);

        // Resetting hero power uses
        CurrentPlayer.Hero.HeroPower.CurrentUses = 0;

        foreach (Minion minion in CurrentPlayer.Minions)
        {
            // Awaking minions and resetting turn attacks
            minion.IsSleeping = false;
            minion.CurrentTurnAttacks = 0;

            // Firing OnTurnStart events
            minion.Buffs.OnTurnStart.OnNext(null);

            // Unfreezing frozen minions or flagging frozen minions for unfreezing on next turn
            if (minion.IsFrozen)
            {
                if (minion.UnfreezeNextTurn)
                {
                    minion.UnfreezeNextTurn = false;
                    minion.IsFrozen = false;
                }
                else
                {
                    minion.UnfreezeNextTurn = true;
                }
            }
        }

        // Drawing 1 card
        CurrentPlayer.Draw();

        // Adding 1 to the turn mana
        CurrentPlayer.AddEmptyMana(1);

        // Moving the overloaded mana crystals to the current turn
        CurrentPlayer.CurrentOverloadedMana = CurrentPlayer.NextOverloadedMana;
        CurrentPlayer.NextOverloadedMana = 0;

        // Refilling mana crystals
        CurrentPlayer.RefillMana();

        // Updating card, hero and minion glows for the current player
        CurrentPlayer.UpdateSprites();

        // Switching to Active Turn state
        CurrentGameState = GameState.Active;

        // TODO : Give the CurrentPlayer the control of the turn
    }

    public void TurnEnd()
    {
        Debugger.Log("Turn end");

        // Switching to End Turn state
        CurrentGameState = GameState.End;

        // Firing OnTurnEnd events
        EventManager.Instance.OnTurnEnd(CurrentPlayer);

        // Resetting hero, card and minion glows for the current player
        CurrentPlayer.ResetSprites();

        // Switching the player
        SwitchCurrentPlayer();

        // Starting the next turn
        TurnStart();
    }

    public void SwitchCurrentPlayer()
    {
        Debugger.Log("Switching current player");

        if (CurrentPlayer == SelfPlayer)
        {
            CurrentPlayer = EnemyPlayer;
        }
        else
        {
            CurrentPlayer = SelfPlayer;
        }
    }

    public List<Minion> GetAllMinions()
    {
        return EnemyPlayer.Minions.Concat(SelfPlayer.Minions).ToList();
    }
}