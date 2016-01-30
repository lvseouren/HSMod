using UnityEngine;
using System.Collections;

public class HeroPower : MonoBehaviour {

	public GameObject token ;
	private bool alreadyUsedThisTurn ;
	public int manaCost ;



	void OnEnable () {
		EventManager.StartListening (EventManager.ON_TURN_START, RefreshHeroPower);
	}

	void OnDisable () {
		EventManager.StopListening (EventManager.ON_TURN_START, RefreshHeroPower);
	}

	void RefreshHeroPower () {
		if (alreadyUsedThisTurn)
			GetComponent<spin>().StartCoroutine (GetComponent<spin>().fliping() );

		alreadyUsedThisTurn = false;
	}

	void OnMouseDown () {
		if (GameLoop.AvaiableMana < 2) {
			Debug.Log ("not enough mana");
			return;
		}
		if (!alreadyUsedThisTurn) {
			GetComponent<spin>().StartCoroutine (GetComponent<spin>().fliping() );
			alreadyUsedThisTurn = true ;
			GetComponent<SummonMinion>().OnBattleCry () ;
		}
		GameLoop.AvaiableMana -= 2;
		EventManager.TriggerEvent (EventManager.ON_MANA_USAGE);
	}




}
