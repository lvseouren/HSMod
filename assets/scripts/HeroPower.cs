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
			GetComponent<Spin>().StartCoroutine (GetComponent<Spin>().fliping() );

		alreadyUsedThisTurn = false;
	}

	void OnMouseDown () {
		if (GameLoop.AvailableMana < 2) {
			Debug.Log ("not enough mana");
			return;
		}
		if (!alreadyUsedThisTurn) {
			GetComponent<Spin>().StartCoroutine (GetComponent<Spin>().fliping() );
			alreadyUsedThisTurn = true ;
			GetComponent<SummonMinion>().OnBattleCry () ;
		}
		GameLoop.AvailableMana -= 2;
		EventManager.TriggerEvent (EventManager.ON_MANA_USAGE);
	}




}
