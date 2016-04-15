using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState;

    public Player TopPlayer;
    public Player BottomPlayer;
    public Player CurrentPlayer;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Start()
    {
        BottomPlayer = new Player();
        TopPlayer = new Player();

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

        if (CurrentPlayer.Equals(BottomPlayer))
        {
            BottomPlayer.Deck.Draw(3);
            TopPlayer.Deck.Draw(4);
            // TODO: give TopPlayer coin
        }
        else
        {
            TopPlayer.Deck.Draw(3);
            BottomPlayer.Deck.Draw(4);
            // TODO: give BottomPlayer coin
        }
    }

    public void TurnStart()
    {
        // Switching to Start Turn state
        CurrentGameState = GameState.Start;
        //EventManager.Instance.OnTurnStart(Player player);

        // Drawing 1 card
        CurrentPlayer.Deck.Draw(1);

        // Suming 1 to the turn mana if it's lower than 10
        if (CurrentPlayer.TurnMana < 10)
        {
            CurrentPlayer.TurnMana++;
        }

        // Refilling mana crystalls
        CurrentPlayer.RefillMana();

        // Switching to Active Turn state
        CurrentGameState = GameState.Active;

        // TODO : Give the CurrentPlayer the control of the turn
    }

    public void TurnEnd()
    {
        // Switching to End Turn state
        CurrentGameState = GameState.End;
        //EventManager.Instance.OnTurnEnd(Player player);

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