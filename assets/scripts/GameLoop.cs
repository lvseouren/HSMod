using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{

	public enum GamePhases
	{
		TURN_START ,
		DRAW_CARD,
		PLAY_TURN ,
		END_TURN 
	}
	
	public static GamePhases currentGamePhase ;
	public int cardsInHand = 3 ;

	public static int myMana = 1, theirMana = 1 ;

	public static int AvailableMana = 1;

	public static bool isMyTurn = true ;

	void Start () {
		for (int i = 0; i < cardsInHand; i++) {
			OnDrawCard ();
		}

		OnTurnStart ();
	}


	public void EndTurn () {
		OnEndTurn ();
		ChangeTurns ();
		OnTurnStart ();
	}

	private void ChangeTurns () {
		isMyTurn = !isMyTurn;
		if (isMyTurn && myMana < 10) {
			myMana ++;
			AvailableMana = myMana;
		} else if (theirMana < 10) {
			theirMana++;
			AvailableMana = theirMana;
		}
	}
	
	public void OnTurnStart () {
		Debug.Log ("OnTurnStart");
		EventManager.TriggerEvent (EventManager.ON_TURN_START);
		currentGamePhase = GamePhases.TURN_START;
		OnDrawCard ();
	}

	public void OnDrawCard () {
		Debug.Log ("OnDrawCard");
		EventManager.TriggerEvent (EventManager.ON_DRAW_CARD);
		currentGamePhase = GamePhases.DRAW_CARD;
		OnPlayTurn ();
	}

	public void OnPlayTurn () {
		Debug.Log ("OnPlayTurn");
		EventManager.TriggerEvent (EventManager.ON_TURN_PLAY);
		currentGamePhase = GamePhases.PLAY_TURN;
	}

	public 	void OnEndTurn () {
		Debug.Log ("OnEndTurn");
		EventManager.TriggerEvent (EventManager.ON_TURN_END);
		currentGamePhase = GamePhases.END_TURN;
	}
}