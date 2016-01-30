using UnityEngine;

public class EndTurn : MonoBehaviour {

	void OnMouseDown () {
		GameObject.FindObjectOfType<GameLoop> ().EndTurn ();
	}
}