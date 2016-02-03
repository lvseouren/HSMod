using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{

	public enum GameState
	{
        MULLIGAN_PHASE,
		TURN_START,
		DRAW_PHASE,
		PLAY_PHASE,
		TURN_END,
        // NYI will be needed later
        BATTLECRY_PHASE,
        DEATHRATTLE_PHASE,
        DEATH_PHASE,
        SUMMON_PHASE, // effect like murloc knight, playing cards in PLAY_PHASE
	    INSPIRE_PHASE,
        ACTIVE_EFFECT // TODO: better name, lightning for example "do something on minion death", cultmaster etc.
    }
	
	public static GameState currentGameState ;
    private Player _player1 = new Player(), _player2 = new Player();
    public Player currentPlayer;

	public int cardsToDraw = 3 ;

	public static int myMana = 1, theirMana = 1 ;

	public static int AvaiableMana = 1;

	public static bool isMyTurn = true ;

	void Start () {
        _player1.Init();
        _player2.Init();

        //TODO: Choose player on mulligan phase
        currentPlayer = _player1; // totally random choice

		for (int i = 0; i < cardsToDraw; i++) {
			OnDrawCard ();
            // TODO: discard
            currentPlayer.cardsInHand += 1;
		}

		OnTurnStart ();
	}

	public void EndTurn () {
		OnEndTurn ();
		ChangeTurns ();
		OnTurnStart ();
	}

	private void ChangeTurns () {
        if (currentPlayer == _player1)
            currentPlayer = _player2;
        else
            currentPlayer = _player1;

        if (currentPlayer.currentMana < currentPlayer.maxMana)
        {
            currentPlayer.currentMana++;
            currentPlayer.RefillMana();
        }
    
	}
	
	public void OnTurnStart () {
		Debug.Log ("OnTurnStart");
		EventManager.TriggerEvent (EventManager.ON_TURN_START);
		currentGameState = GameState.TURN_START;
		OnDrawCard ();
	}

	public void OnDrawCard () {
		Debug.Log ("OnDrawCard");
		EventManager.TriggerEvent (EventManager.ON_DRAW_CARD);
		currentGameState = GameState.DRAW_PHASE;
		OnPlayTurn ();
	}

	public void OnPlayTurn () {
		Debug.Log ("OnPlayTurn");
		EventManager.TriggerEvent (EventManager.ON_TURN_PLAY);
		currentGameState = GameState.PLAY_PHASE;
	}

	public 	void OnEndTurn () {
		Debug.Log ("OnEndTurn");
		EventManager.TriggerEvent (EventManager.ON_TURN_END);
		currentGameState = GameState.TURN_END;
	}
}