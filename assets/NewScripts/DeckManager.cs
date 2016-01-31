using UnityEngine;
using System;
//Required for using lists
using System.Collections.Generic;
using System.Collections;

// Please correct me if I do something stupid or wrong :D
//
// This script will be used to manage all things deck related when in game.
// Includes:
//      - Card draw / Card burn / Discard
//      - Card positioning and shifting in hand
//      - Card related animations (e.g. card draw, holding card mid-air, hovering over card)
//      - Total card amount / Card shuffling / Card additions 
//      - How the deck model looks depending on the remaining number of cards
//
//

public class DeckManager : MonoBehaviour {

    //This list will hold all the cards; every card will be a seperate gameobject.
    //Lists are more flexible and dynamic than arrays (in c# at least) so theyre probably the better option.
    //Size will start at 30, and will collapse or grow depending on how many cards are left.
    //Oh, this way would also make it easier to make your own decks.

    public List<GameObject> allCardsInDeck = new List<GameObject>();
    public int remainingCards;

    /*
        We should lerp the position between these two (both position and rotation) 
        These positions will also be changed for the oppponent.
    */

    //This will hold the initial position for when cards are drawn from the deck.
    public Transform newCardPosition;
    //This will hold the position of the card just when it is drawn.
    public Transform newCardShownPosition;
    //The speed of lerping.
    public float lerpSpeed;
    //I think its easier to just change the lerp location with a timer instead of checking the position, so this is the timer.
    float lerpTimer;

    //Just some checks
    int drawMove = 0;

    GameObject setCard;
    GameObject setCardCloned;
    Transform emptyHandSlot;

    public GameObject myHand;
    HandManager handManager;

    public float zOffset, yOffset, angleVariation, xDistance, powFactor;
    public float zRotation, yRotation, xRotation;




    void Start ()
    {
        //
        handManager = myHand.GetComponent<HandManager>();

        //Shoud be 30 on start.
        remainingCards = allCardsInDeck.Count;

        //Placeholder - need to check card draw
        Draw();
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire2")) Draw();

        lerpTimer += 1f * Time.deltaTime;

        //Create a new card and have it positioned + rotated in the deck.
        if (drawMove == 1)
        {
            print("Created");
            setCardCloned = Instantiate(setCard, newCardPosition.transform.position, newCardPosition.transform.rotation) as GameObject;
            handManager.cardsInHand.Add(setCardCloned);
            drawMove = 2;
        }

        if (drawMove == 2)
        {
            if (lerpTimer > 0f)
            {
                if(lerpTimer > 1.5f)
                {
                    print("Moving to hand");

                    emptyHandSlot = handManager.emptySlot;
                    setCardCloned.transform.position = Vector3.Slerp(setCardCloned.transform.position, emptyHandSlot.transform.position, lerpSpeed * Time.deltaTime);
                    setCardCloned.transform.rotation = Quaternion.Lerp(setCardCloned.transform.rotation, emptyHandSlot.transform.rotation, lerpSpeed * Time.deltaTime);

                    /*
                        Equation to move and shift into hand should go somewhere here
                    
                    */
                }
                else
                {
                    print("Moving to shown position");
                    setCardCloned.transform.position = Vector3.Lerp(setCardCloned.transform.position, newCardShownPosition.transform.position, lerpSpeed * Time.deltaTime);
                    setCardCloned.transform.rotation = Quaternion.Lerp(setCardCloned.transform.rotation, newCardShownPosition.transform.rotation, lerpSpeed * Time.deltaTime);
                }
            }

            if (lerpTimer > 3f)
            {
                setCard = null;
                setCardCloned = null;
                drawMove = 0;
            }
        }
    }

    //Unlike in real HS, as of now I think it would be better to have the draw function go as so, without an actual set deck.
    public void Draw()
    {
        //Check if there are any cards in the deck, otherwise start fatiguing.
        if(remainingCards > 0)
        {
            //Get a random card
            //----(Have to add by 1 because the second number is exclusive)
            int randomNumber = UnityEngine.Random.Range(0, remainingCards /* + 1*/ );

            //Set the drawn card
            setCard = allCardsInDeck[randomNumber];

            //Initiate Movement
            drawMove = 1;

            //Remove the card from the deck
            allCardsInDeck.RemoveAt(randomNumber);

            //Refresh amount of cards
            remainingCards = allCardsInDeck.Count;
        }
        else
        {
            Fatigue();
        }
    }

    public void Discard()
    {

    }

    public void Fatigue()
    {

    }


}
