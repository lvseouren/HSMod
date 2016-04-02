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

        Mulligan();
    }

    public void Mulligan()
    {
        CurrentGameState = GameState.Mulligan;

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


// WARNING : Probably won't need gamestates anyways, but we'll keep them for now

/* Active state of the game
 * MULLIGAN happens once at the start of match
 * START is when a specific player's turn start
 * ACTIVE happens between START and END
 * END is when a specific player's turn ends
 */
public enum GameState
{
    Mulligan,
    Start,
    End,
    Active
}