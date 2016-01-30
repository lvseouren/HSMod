using UnityEngine;
using System.Collections;

public class CardInHand : MonoBehaviour {

	protected Vector3 originalLocalScale;
	protected Vector3 position ;
	protected Vector3 rotation ;
	
	protected Vector3 screenPoint;
	protected Vector3 offset;
	
	protected int index = 1;
	protected Vector3[] minionsPositions ;
	
	protected  MeshRenderer preview ;
	
	protected GameObject myBf ;
	protected float minionHeigh ;
	protected float minionWidth ;
	protected Vector3 curScreenPoint;
	protected Vector3 curPosition;
	protected GameObject empty ;

	void Start () {
		preview = GameObject.FindGameObjectWithTag ("PreviewImage").GetComponent<MeshRenderer>();
		myBf = GameObject.FindGameObjectWithTag ("MyBF");
	}


	/*
		CENTER THE CARD IN THE MOUSE, AND SETUP SOME VARIABLES
	 */
	protected virtual void OnMouseDown () {

		if (CardsInHandState.currentState == CardsInHandState.States.DRAWING)
			return;
		
		if (GameLoop.currentGamePhase != GameLoop.GamePhases.PLAY_TURN)
			return;
		if (!GameLoop.isMyTurn) {
			Debug.Log ("Its not my turn");
			return;
		}

		originalLocalScale = transform.localScale;
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		position = transform.localPosition;
		Vector3 temp = transform.localPosition;
		temp.y += 10;
		transform.localPosition = temp;
		rotation = transform.eulerAngles;
		transform.eulerAngles = new Vector3 (0, 0, 0);
		CardsInHandState.currentState = CardsInHandState.States.HOLDING_CARD;
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		GetComponentInChildren<MeshRenderer> ().enabled = true;
		preview.enabled = false;
	}

	/*
		DRAG THE CARD AROUND
	 */

	protected virtual void OnMouseDrag () {
		if (CardsInHandState.currentState != CardsInHandState.States.HOLDING_CARD)
			return;
		if (GameLoop.currentGamePhase != GameLoop.GamePhases.PLAY_TURN)
			return;
		if (!GameLoop.isMyTurn) {
			Debug.Log ("Its not my turn");
			return;
		}

		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);


		transform.position = curPosition;

	}

	protected virtual void PlayCard () {

	}

	/* 
	 * RETURN THE CARD TO THE INITIAL POSITION, IF WE DRAG THE CARD INSIDE PLAY AREA, CALL PlayCard
	 * TO OVERRIDE THE FUNCTIONALITY, OVERRIDE PLAYCARD FUNCTION IN YOUR SCRIPT
	*/

	IEnumerator OnMouseUp () {

		GameObject.Destroy (empty);
		yield return null;
		
		if (GameLoop.currentGamePhase != GameLoop.GamePhases.PLAY_TURN)
			yield break;
		
		if (!GameLoop.isMyTurn) {
			Debug.Log ("Its not my turn");
			yield break;
		}

		if (GetComponent<MinionCard> ()) {
			if (GameLoop.AvailableMana < GetComponent<MinionCard> ().manaCost) {
				Debug.Log ("Not enough mana");
				yield break;
			}
		}else if (GetComponent<PlaySpellInHand>()){

		}




		if (myBf.transform.childCount > 6) {
			Debug.Log ("My board is full");
			yield break;
		}
		
		
		if (CardsInHandState.currentState == CardsInHandState.States.DRAWING)
			yield break;
		
		RaycastHit[] hits;
		hits = Physics.RaycastAll( Camera.main.ScreenToWorldPoint ( Input.mousePosition ) , Vector3.down , 1000.0F);

		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			if (hit.collider.tag == "PlayCardArea"){

				PlayCard () ;

			}
		}

		GameObject.FindGameObjectWithTag ("MyBF").GetComponent<ArrangeChildren>().ArrangeCards();

		transform.localPosition = position;
		transform.localScale = originalLocalScale;
		transform.eulerAngles = rotation;
		CardsInHandState.currentState = CardsInHandState.States.NOT_HOLDING_CARD;
		
	}

	/* 
	 * HERE WE HAVE THE HOVER-OVER CARD EFFECT 
	*/

	void OnMouseEnter () {
		if (CardsInHandState.currentState != CardsInHandState.States.NOT_HOLDING_CARD) {
			return ;
		}
		GetComponentInChildren<MeshRenderer> ().enabled = false;
		preview.gameObject.transform.localPosition = new Vector3 (transform.localPosition.x ,
		                                                          preview.transform.localPosition.y,
		                                                          preview.transform.localPosition.z);	
		preview.materials = this.GetComponentInChildren<MeshRenderer> ().materials;
		preview.enabled = true;
		
	}
	
	void OnMouseExit () {
		if (CardsInHandState.currentState != CardsInHandState.States.NOT_HOLDING_CARD ) 
			return ;
		GetComponentInChildren<MeshRenderer> ().enabled = true;
		preview.enabled = false;
		
	}
}
