using UnityEngine;
using System.Collections;

public class PlayMinionInHand : CardInHand {

    // commented old guys stuff giving errors since we are probably still going to throw this away.

	protected override void OnMouseDown () {
        /*
		if (GameLoop.AvaiableMana < GetComponent<MinionCard> ().manaCost) {
			Debug.Log ("Not enough mana");
			return;
		}*/

		base.OnMouseDown ();

		if (myBf.transform.childCount > 6) {
			Debug.Log ("My board is full");
			return;
		}

		minionsPositions = new Vector3[myBf.transform.childCount];

		int i = 0;
		foreach (Transform t in GameObject.FindGameObjectWithTag ("MyBF").transform ) {
			minionsPositions[i] = t.transform.position ;
			i++;
		}

		if (GetComponent<MinionCard> () != null && myBf.transform.childCount > 0 ) {
			empty = (GameObject) Instantiate (myBf.transform.GetChild(0).gameObject ,
			                                  myBf.transform.GetChild(0).position,
			                                  myBf.transform.GetChild(0).rotation);
			empty.SetActive ( false );
			empty.transform.SetParent ( GameObject.FindGameObjectWithTag ("MyBF").transform);
		}

		if (myBf.transform.childCount > 0) {
			minionHeigh = myBf.transform.GetChild (0).GetComponent<BoxCollider> ().bounds.size.z;
			minionWidth = myBf.transform.GetChild (0).GetComponent<BoxCollider> ().bounds.size.x;
		}

	}

	protected override void OnMouseDrag () {
        /*
		if (GameLoop.AvaiableMana < GetComponent<MinionCard>().manaCost) {
			Debug.Log ("Not enough mana");
			return;
		}*/

		base.OnMouseDrag ();

		if (myBf.transform.childCount > 7)
			return;
		if (myBf.transform.childCount == 0) {
			index = 0 ;
		}

		else if (curPosition.z > minionsPositions [0].z - minionHeigh / 2 && curPosition.z < minionsPositions [0].z + minionHeigh / 2) {
			index = LeftFromWho ( curPosition.x );
			empty.transform.SetSiblingIndex (index);
			myBf.GetComponent<ArrangeChildren> ().ArrangeCards ();
		} 

	}

	int LeftFromWho (float xPosition) {
		for (int i = 0; i < minionsPositions.Length; i++) {
			if (xPosition < minionsPositions[i].x) {
				return i ;
			}
		}
		return minionsPositions.Length;
	}

	protected override void PlayCard ()
	{
		GetComponent<MinionCard>().OnPlay (index);
		GetComponentInParent<ArrangeChildren>().StartCoroutine (
			GetComponentInParent<ArrangeChildren>().delayArrange ());
		//GameLoop.AvaiableMana -= GetComponent<MinionCard> ().manaCost;
		EventManager.TriggerEvent(EventManager.ON_MANA_USAGE);
		GameObject.Destroy (this.gameObject);
	}





}
