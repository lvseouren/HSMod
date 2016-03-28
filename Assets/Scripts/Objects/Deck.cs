using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public GameObject DeckObject;
    public GameObject HandObject;

    private List<GameObject> _currentDeck = new List<GameObject>();

    private void Start()
    {
        // Looping on all cards of the Deck
        for (int i = 0; i < DeckObject.transform.childCount; i++)
        {
            // Adding the card to the current deck
            _currentDeck.Add(DeckObject.transform.GetChild(i).gameObject);
        }

        // Shuffling all the cards in the deck
        for (int t = 0; t < _currentDeck.Count; t++)
        {
            GameObject tmp = _currentDeck[t];
            int random = Random.Range(t, _currentDeck.Count);
            _currentDeck[t] = _currentDeck[random];
            _currentDeck[random] = tmp;
        }

        // Drawing the 3 starting cards
        Draw(3);
    }

    public void Draw(int draws)
    {
        // Looping x number of times
        for (int i = 0; i < draws; i++)
        {
            // Drawing a card
            Draw();
        }
    }

    public void Draw()
    {
        // Instantiating a new card
        GameObject drawnCard = Instantiate(_currentDeck[0]);
        drawnCard.transform.localScale = drawnCard.transform.localScale / 2;
        drawnCard.transform.SetParent(HandObject.transform);

        // Removing the instantiated card from the deck
        _currentDeck.RemoveAt(0);
    }
}