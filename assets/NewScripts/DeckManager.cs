using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour {

	public GameObject deck;
	public GameObject yourHand;

	public List<GameObject> _fullDeck = new List<GameObject>();

	void Start () {
		for(int i = 0; i < deck.transform.childCount; i++) {
			_fullDeck.Add (deck.transform.GetChild(i).gameObject);
		}

		for (int t = 0; t < _fullDeck.Count; t++ ) {
			var tmp = _fullDeck[t];
			int r = Random.Range(t, _fullDeck.Count);
			_fullDeck[t] = _fullDeck[r];
			_fullDeck[r] = tmp;
		}

		Draw ();
		Draw ();
		Draw ();
	}

	public void Draw() {
		GameObject newCard = Instantiate (_fullDeck [0]);
		_fullDeck.Remove (_fullDeck [0]);
		newCard.transform.localScale = newCard.transform.localScale / 2;
		newCard.transform.SetParent (yourHand.transform);
	}
}