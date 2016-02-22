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
        IDLE,
        DEATH_PHASE,
    }
	
	public static GameState currentGameState ;
    private Player _player1 = new Player(), _player2 = new Player();
    public Player currentPlayer;

	public int cardsToDraw = 3 ;

	void Start () {
        _player1.Init();
        _player2.Init();

        //TODO: Choose player on mulligan phase
        currentPlayer = _player1; // totally random choice

		for (int i = 0; i < cardsToDraw; i++) {
			DrawPhase();
            // TODO: discard
            currentPlayer.cardsInHand += 1;
		}

		TurnStart ();
	}

	public void EndTurn () {
		TurnEnd ();
		ChangeTurns ();
		TurnStart ();
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

    // others states will be changed by playing cards etc.
    // need to find the right order of going through phases.

    public void TurnStart()
    {
        Debug.Log("Turn Start");
        currentGameState = GameState.TURN_START;
        // fire events here that should be fired at turn start like Demolisher's effect


        DrawPhase();
    }

    public void DrawPhase()
    {
        Debug.Log("Draw Phase");
        currentGameState = GameState.DRAW_PHASE;
        // is discard done?
        currentPlayer.deck.Draw(); // in this function fire events that should start when u draw a card. for example Shadow beast from priest decks
        // any other potential effects on Draw?
        currentGameState = GameState.IDLE;
    }

    // should be called by EndTurn
    // which should be called by pressing end turn button NYI
    public void TurnEnd()
    {
        Debug.Log("Turn End");
        currentGameState = GameState.TURN_END;
        // fire events here which should be fired at turn end like Ragnaros

    }


    // STUFF DONE BY THE OLD GUY.
    // MAYBE ONE DAY IT WILL BE NEEDED
    // OR WHEN WE FIGURE OUT HOW TO USE EVENTS
    /* 
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
    */
}