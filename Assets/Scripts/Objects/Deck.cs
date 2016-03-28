using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public GameObject _Deck;
    public GameObject Hand;

    private List<GameObject> _currentDeck = new List<GameObject>();

    private void Start()
    {
        // Looping on all cards of the Deck
        for (int i = 0; i < _Deck.transform.childCount; i++)
        {
            // Adding the card to the current deck
            _currentDeck.Add(_Deck.transform.GetChild(i).gameObject);
        }

        // Shuffling all the cards in the deck
        for (int t = 0; t < _currentDeck.Count; t++)
        {
            GameObject tmp = _currentDeck[t];
            int r = Random.Range(t, _currentDeck.Count);
            _currentDeck[t] = _currentDeck[r];
            _currentDeck[r] = tmp;
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
        drawnCard.transform.SetParent(Hand.transform);

        // Removing the instantiated card from the deck
        _currentDeck.RemoveAt(0);
    }
}