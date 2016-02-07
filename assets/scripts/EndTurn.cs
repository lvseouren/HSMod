using UnityEngine;
using System.Collections;

public class EndTurn : MonoBehaviour {

	void OnMouseDown () {
		GameObject.FindObjectOfType<GameLoop> ().EndTurn ();
	}

}