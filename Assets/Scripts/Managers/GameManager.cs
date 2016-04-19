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

        BottomPlayer = Player.Create(HeroClass.DeathKnight, new Vector3(797f, 60f, 230f), new Vector3(800f, 60f, 50f));
        TopPlayer = Player.Create(HeroClass.DeathKnight, new Vector3(800f, 60f, 935f), new Vector3(800f, 60f, 1175f));

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
        EventManager.Instance.OnTurnStart(this.CurrentPlayer);

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
        EventManager.Instance.OnTurnEnd(this.CurrentPlayer);

        // Resetting hero, card and minion glows for the current player
        CurrentPlayer.ResetGlows();

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

    public List<MinionCard> GetAllMinions()
    {
        return TopPlayer.Minions.Concat(BottomPlayer.Minions).ToList();
    }
}