using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaTextUpdate : MonoBehaviour {

	public bool MyManaText ;

	void OnEnable () {
		EventManager.StartListening (EventManager.ON_TURN_START, UpdateText);
		EventManager.StartListening (EventManager.ON_MANA_USAGE, UpdateText);
	}

	void OnDisable () {
		EventManager.StopListening (EventManager.ON_TURN_START, UpdateText);
		EventManager.StopListening (EventManager.ON_MANA_USAGE, UpdateText);
	}

	void UpdateText () {
		if (GameLoop.isMyTurn && MyManaText)
			GetComponentInChildren<Text> ().text = GameLoop.AvailableMana + "/" + GameLoop.myMana;
		else if (!GameLoop.isMyTurn && !MyManaText )
			GetComponentInChildren<Text> ().text = GameLoop.AvailableMana + "/" + GameLoop.theirMana;
	}
}
