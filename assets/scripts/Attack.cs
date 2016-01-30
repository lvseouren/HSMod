using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	private GameObject pointer ;
	private LineRenderer line;

	public bool canTargetAllies , canTargetHero , canTargetOponents ;
	public bool isSleeping ;
	public bool alreadyAttackedThisTurn ;

	Vector3 screenPoint , curScreenPoint , curPosition , aux;

	void Awake () {
		pointer = GameObject.FindGameObjectWithTag ("Seta");
		if (GetComponent<Hero> ())
			return;
		if (GetComponent<BasicMinionSuperClass> ().hasCharge) {
			isSleeping = false;
		} else {
			isSleeping = true ;
		}
	}

	void OnEnable () {
		EventManager.StartListening ( EventManager.ON_TURN_START , onNewTurn  );
	}

	void OnDisable () {
		EventManager.StopListening (EventManager.ON_TURN_START, onNewTurn);
	}

	void onNewTurn () {
		isSleeping = false;
		alreadyAttackedThisTurn = false;
	}


	void Start () {
		line = pointer.GetComponent<LineRenderer> ();
	}

	void OnMouseDown () {
		if (alreadyAttackedThisTurn)
			return;
		if (isSleeping)
			return;

		line.enabled = true;
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		pointer.transform.position = curPosition;
		line.SetPosition (0 , new Vector3 (this.transform.position.x , 10f , this.transform.position.z) );
		line.SetPosition (1 , curPosition);
	}

	void OnMouseDrag () {
		if (alreadyAttackedThisTurn)
			return;
		if (isSleeping)
			return;
		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		line.SetPosition (1 , curPosition);
	}

	void OnMouseUp () {
		if (alreadyAttackedThisTurn)
			return;
		if (isSleeping)
			return;
		line.enabled = false;
		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		RaycastHit[] hits;
		hits = Physics.RaycastAll( Camera.main.ScreenToWorldPoint ( Input.mousePosition ) , Vector3.down , 1000.0F);
		//bool played = false ;
		for (int i = 0; i < hits.Length; i++) {
			if(hits[i].transform.name == "TheirHero"){

				StartCoroutine (attack ( hits[i].transform.position , hits[i].transform.gameObject) );
                
				alreadyAttackedThisTurn = true ;
			}else if (hits[i].transform.parent!= null && hits[i].transform.parent.name == "TheirSideOfBf"){

				StartCoroutine (attack (hits[i].transform.position , hits[i].transform.gameObject) );
                
			}
		}
	}

	IEnumerator attack (Vector3 finalPosition , GameObject hit) {

		finalPosition.z -= GetComponent<BoxCollider> ().bounds.size.z/2;
		Vector3 initialPosition = this.transform.position;

		float Distance = Vector2.Distance ( new Vector2 ( this.transform.position.x , this.transform.position.z ) ,
		                                   new Vector2 (  finalPosition.x ,  finalPosition.z ));


		while (Vector3.Distance ( this.transform.position , finalPosition ) > 0.01f) {
			this.transform.position = Vector3.MoveTowards ( this.transform.position , finalPosition, Distance * Time.deltaTime * 10) ;
			yield return null;
		}
		if (hit.GetComponent<Hero> () != null) {
			hit.GetComponent<Hero> ().currentHp -= GetComponent<BasicMinionSuperClass> ().currentAttack;
		} else if (hit.GetComponent<BasicMinionSuperClass> () != null) {
			GetComponent<BasicMinionSuperClass>().currentHealth -= 
				hit.GetComponent<BasicMinionSuperClass>().currentAttack ;
			hit.GetComponent<BasicMinionSuperClass>().currentHealth -=
				GetComponent<BasicMinionSuperClass>().currentAttack;
		}
        
		alreadyAttackedThisTurn = true ;

		while (Vector3.Distance ( this.transform.position , initialPosition ) > 0.01f) {
			this.transform.position = Vector3.MoveTowards ( this.transform.position , initialPosition, Distance * Time.deltaTime * 10) ;
			yield return null;
		}
	}
}