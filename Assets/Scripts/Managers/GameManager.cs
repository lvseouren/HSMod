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

    public Player TopPlayer;
    public Player BottomPlayer;
    public Player CurrentPlayer;
    
    public void Start()
    {
        _instance = this;

        // TODO : Move quality stuff to new class
        QualitySettings.vSyncCount = 1;

        #region Test Zone

        Vector3 bottomCenter = new Vector3(798f, 60f, 230f);
        Vector3 bottomHand = new Vector3(0f, -11f, 0f);
        Vector3 bottomMana = new Vector3(8.05f, -3.25f, 0f);

        BottomPlayer = Player.Create(HeroClass.DeathKnight, bottomCenter, bottomHand, bottomMana, true);

        BottomPlayer.Hero.HeroPower = new RaiseGhoul(BottomPlayer.Hero);

        BottomPlayer.Deck = new List<BaseCard>()
        {
            new CorpseExplosion(),
            new AllWillServe(),
            new DancingRuneblade(),
            new SkeletonCommander(),
            new DeathwhisperNecrolyte(),
            new CorpseExplosion(),
            new AllWillServe(),
            new DancingRuneblade(),
            new SkeletonCommander(),
            new DeathwhisperNecrolyte()
        };

        Vector3 topCenter = new Vector3(800f, 60f, 935f);
        Vector3 topHand = new Vector3(0f, 11f, 0f);
        Vector3 topMana = new Vector3(8.05f, 3.25f, 0f);

        TopPlayer = Player.Create(HeroClass.DeathKnight, topCenter, topHand, topMana, false);

        TopPlayer.Hero.HeroPower = new RaiseGhoul(TopPlayer.Hero);

        TopPlayer.Deck = new List<BaseCard>()
        {
            new CorpseExplosion(),
            new AllWillServe(),
            new DancingRuneblade(),
            new SkeletonCommander(),
            new DeathwhisperNecrolyte(),
            new CorpseExplosion(),
            new AllWillServe(),
            new DancingRuneblade(),
            new SkeletonCommander(),
            new DeathwhisperNecrolyte()
        };

        #endregion

        BottomPlayer.Enemy = TopPlayer;
        TopPlayer.Enemy = BottomPlayer;

        // Randomize the starting player
        if (Random.Range(0, 2) == 1)
        {
            CurrentPlayer = TopPlayer;
        }
        else
        {
            CurrentPlayer = BottomPlayer;
        }

        Mulligan();
    }

    public void Mulligan()
    {
        CurrentGameState = GameState.Mulligan;

        // TODO : Rework mulligan

        if (CurrentPlayer.Equals(BottomPlayer))
        {
            BottomPlayer.Draw(3);
            TopPlayer.Draw(4);
            // TODO: Give TopPlayer coin
        }
        else
        {
            TopPlayer.Draw(3);
            BottomPlayer.Draw(4);
            // TODO: Give BottomPlayer coin
        }
    }

    public void TurnStart()
    {
        // Switching to Start Turn state
        CurrentGameState = GameState.Start;

        // Firing OnTurnStart events
        EventManager.Instance.OnTurnStart(CurrentPlayer);

        // Drawing 1 card
        CurrentPlayer.Draw();

        // Suming 1 to the turn mana if it's lower than 10
        if (CurrentPlayer.TurnMana < 10)
        {
            CurrentPlayer.TurnMana++;
        }

        // Refilling mana crystalls
        CurrentPlayer.RefillMana();

        // Updating card, hero and minion glows for the current player
        CurrentPlayer.UpdateGlows();

        // Switching to Active Turn state
        CurrentGameState = GameState.Active;

        // TODO : Give the CurrentPlayer the control of the turn
    }

    public void TurnEnd()
    {
        // Switching to End Turn state
        CurrentGameState = GameState.End;

        // Firing OnTurnEnd events
        EventManager.Instance.OnTurnEnd(CurrentPlayer);

        // Resetting hero, card and minion glows for the current player
        CurrentPlayer.ResetGreenGlows();

        // Switching the player
        SwitchCurrentPlayer();

        // Starting the next turn
        TurnStart();
    }

    public void SwitchCurrentPlayer()
    {
        if (CurrentPlayer == BottomPlayer)
        {
            CurrentPlayer = TopPlayer;
        }
        else
        {
            CurrentPlayer = BottomPlayer;
        }
    }

    public List<Minion> GetAllMinions()
    {
        return TopPlayer.Minions.Concat(BottomPlayer.Minions).ToList();
    }

    public void UpdateAll()
    {
        foreach (Minion minion in GetAllMinions())
        {
            minion.Controller.UpdateNumbers();
            minion.Controller.UpdateSprites();
        }

        TopPlayer.UpdateAll();
        BottomPlayer.UpdateAll();
    }
}