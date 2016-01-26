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
    public Transform newCardPos;
    //This will hold the position of the card just when it is drawn.
    public Transform newCardShownPosition;
    //The speed of lerping.
    public float lerpSpeed;
    //I think its easier to just change the lerp location with a timer instead of checking the position, so this is the timer.
    public float lerpTimer;



    void Start ()
    {
        //Shoud be 30 on start.
        remainingCards = allCardsInDeck.Count;
    }
	
	void Update ()
    {
	
	}

    //Unlike in real HS, as of now I think it would be better to have the draw function go as so, without an actual set deck.
    public void Draw()
    {
        //Check if there are any cards in the deck, otherwise start fatiguing.
        if(remainingCards > 0)
        {
            //Get a random card
            //(Have to add by 1 because the second number is exclusive)
            int randomNumber = UnityEngine.Random.Range(0, remainingCards + 1);

            //Set the drawn card
            GameObject drawnCard = allCardsInDeck[randomNumber];

            AddCardToHand(drawnCard);

        }
        else
        {
            Fatigue();
        }
    }

    //Requires a card
    public void AddCardToHand(GameObject setCard)
    {
        //Create a new card and have it positioned + rotated in the deck.
        Instantiate(setCard, newCardPos.transform.position, newCardPos.transform.rotation);
    }

    public void Discard()
    {

    }

    public void Fatigue()
    {

    }


}
