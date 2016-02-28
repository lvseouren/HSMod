using UnityEngine;
using System.Collections;
using System.Linq;

public class Deck : MonoBehaviour {
	
	private GameObject[] myDeckCards ;

	public GameObject myDeck ;
	public GameObject myHand ;

	public GameObject theirHand ;

	void Awake () {

		myDeckCards = Resources.LoadAll ("Cards/MyDeck/" , typeof(GameObject))
			.Cast<GameObject>()
				.ToArray();
	}

	void OnEnable () {
		EventManager.StartListening (EventManager.ON_DRAW_CARD, DrawCard );
	}

	void OnDisable () {
		EventManager.StopListening (EventManager.ON_DRAW_CARD, DrawCard );
	}

	void DrawCard () {
		StartCoroutine (drawCard ());
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.G) ){
            Debug.Log(myDeckCards.Length);
		}
	}

	IEnumerator drawCard () {
        int randomNr = Random.Range(0, myDeckCards.Length);

		GameObject temp = (GameObject)Instantiate ( myDeckCards[randomNr] , myDeck.transform.position , myDeck.transform.rotation );
        if (GameLoop.currentPlayer == GameLoop._player1) // assuming player1 is us
            reduceDeck(myDeckCards, randomNr);
		temp.transform.Rotate ( new Vector3 ( 0 , 0 , 0) );

		CardsInHandState.currentState = CardsInHandState.States.DRAWING;
		Vector3 originalLocalScale = temp.transform.localScale;
		temp.transform.localScale = new Vector3 (0.01f , 0.01f , 0.01f);
		yield return new WaitForSeconds (0.1f);

		Vector3 finalPosition = new Vector3 (9.5f , 1f , -2.8f);
		Vector3 scale = new Vector3 (0.6f, 0.6f, 0.6f);
		Vector3 rotation = new Vector3 (0, 0, 0);

		float time = 0; 
		while (time < 1.3f) {
			temp.transform.localScale = Vector3.Lerp ( temp.transform.localScale , scale , Time.deltaTime * 9 );
			temp.transform.eulerAngles = Vector3.Lerp (temp.transform.eulerAngles , rotation , Time.deltaTime * 30	 );
			temp.transform.position = Vector3.Lerp (temp.transform.position , finalPosition , Time.deltaTime * 9 );
			time += Time.deltaTime;
			yield return null ;
		}

		temp.transform.localScale = originalLocalScale;

		if (GameLoop.currentPlayer == GameLoop._player1) //assuming player1 is us 
        {
			temp.transform.SetParent (myHand.transform);
			myHand.GetComponent<ArrangeChildren> ().ArrangeCards ();
		} else {
			temp.transform.SetParent (theirHand.transform);
			theirHand.GetComponent<ArrangeChildren>().ArrangeCards();
		}


		CardsInHandState.currentState = CardsInHandState.States.NOT_HOLDING_CARD;
	}
    void reduceDeck(GameObject[] oldArray, int randomNr)
    {
        if (oldArray.Length >0)
        { 
            GameObject[] tempArray = new GameObject[oldArray.Length - 1];
            for (int i=0; i<randomNr; i++)
            {
                tempArray[i] = oldArray[i];
            }
            for (int i = randomNr; i < tempArray.Length; i++)
            {
                tempArray[i] = oldArray[i+1];
            }
            myDeckCards = tempArray;
        }
        
    }




}