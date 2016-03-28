using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState;
    public Player CurrentPlayer;

    private Player _topPlayer;
    private Player _bottomPlayer;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!_instance)
                {
                    Debug.LogError("NO GameManager instance found on scene.");
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

        // Randomize the starting player
        if (Random.Range(0, 2) == 1)
        {
            CurrentPlayer = _topPlayer;
        }
        else
        {
            CurrentPlayer = _bottomPlayer;
        }
    }

    public void TurnStart()
    {
        // Start up after Mulligan
        if (CurrentGameState == GameState.Mulligan)
        {
            // TODO : Move this section to a separate one, that allows discarding cards and such stuff (Mulligan)
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
        // TODO: Fire OnTurnStart event

        CurrentPlayer.Deck.Draw(1);
        // TODO : Fire OnCardDrawn event

        if (CurrentPlayer.CurrentMana < 10)
        {
            CurrentPlayer.CurrentMana++;
        }

        CurrentPlayer.RefillMana();

        CurrentGameState = GameState.Active;
    }

    public void TurnEnd()
    {
        CurrentGameState = GameState.End;
        // TODO: Fire OnTurnEnded event

        ChangePlayers();
        TurnStart();
    }

    public void ChangePlayers()
    {
        if (CurrentPlayer == _bottomPlayer)
        {
            CurrentPlayer = _topPlayer;
        }
        else
        {
            CurrentPlayer = _bottomPlayer;
        }
    }
}

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