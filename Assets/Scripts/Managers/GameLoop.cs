using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    /* Active state of the game
     * MULLIGAN happens once at the start of match
     * START is when a specific player's turn start
     * END is when a specific player's turn ends
     * ACTIVE happens between START and END
     */
    public enum GameState
    {
        Mulligan,
        Start,
        End,
        Active
    }

    public GameState CurrentGameState;
    public Player CurrentPlayer;

    private Player _topPlayer;
    private Player _bottomPlayer;

    private static GameLoop _instance;

    public static GameLoop Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameLoop)) as GameLoop;
                if (!_instance)
                {
                    Debug.LogError("NO GameLoop instance found on scene.");
                }

            }
            return _instance;
        }
    }

    public void Start()
    {
        CurrentGameState = GameState.Mulligan;

        _bottomPlayer = new Player();
        _topPlayer = new Player();

        _bottomPlayer.Init();
        _topPlayer.Init();

        // Choose starting player
        if (Random.Range(0, 2) == 1)
            CurrentPlayer = _topPlayer;
        else
            CurrentPlayer = _bottomPlayer;

    }

    public void TurnStart()
    {
        // what happens right after mulligan
        if (CurrentGameState == GameState.Mulligan)
        {
            if (CurrentPlayer.Equals(_bottomPlayer))
            {
                _bottomPlayer.Deck.Draw(3);
                _topPlayer.Deck.Draw(4);
                // TODO: give _topPlayer coin
            }
            else
            {
                _topPlayer.Deck.Draw(3);
                _bottomPlayer.Deck.Draw(4);
                // TODO: give _bottomPlayer coin
            }
        }

        CurrentGameState = GameState.Start;
        // TODO: fire all the necessary events?

        CurrentPlayer.Deck.Draw(1);

        if (CurrentPlayer.CurrentMana < 10)
            CurrentPlayer.CurrentMana++;

        CurrentPlayer.RefillMana();

        CurrentGameState = GameState.Active;
    }

    public void TurnEnd()
    {
        CurrentGameState = GameState.End;
        // TODO: fire all necessary events?

        ChangePlayers();
        TurnStart();
    }

    public void ChangePlayers()
    {
        if (CurrentPlayer.Equals(_bottomPlayer))
            CurrentPlayer = _topPlayer;
        else
            CurrentPlayer = _bottomPlayer;
    }
}