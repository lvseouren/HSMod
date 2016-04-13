using UnityEngine;

// TODO : Rewrite whole class
public class DeckBuilder : MonoBehaviour
{
    public GameObject[] Collection;
    public GameObject[] DisplayedCards = new GameObject[8];
    public GameObject[] Deck = new GameObject[30];

    // starting positions for showing cards
    // currently on scene
    // x = -7 and Y = 4
    // spawn other cards on 5 x axis difference and 8 y axis difference
    private const int _firstCardXCoordinate = -7;
    private const int _firstCardYCoordinate = 4;

    private void Start()
    {
        // assuming we already got collection from somewhere
        TESTING_ONLY_FILL_ARRAY();

        if (Collection != null)
        {
            Display();
        }

    }

    private void TESTING_ONLY_FILL_ARRAY()
    {
        if (Collection == null)
        {
            Debug.LogError("TESTING ONLY ERROR");
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            DisplayedCards[i] = Collection[0];
        }
    }

    public void AddToDeck()
    {

    }

    private void Display()
    {
        for (int i = 0; i < 8; i++)
        {
            int _newX = _firstCardXCoordinate + (i * 5) % 20;
            int _newY = _firstCardYCoordinate - ((int)i / 4) * 8;
            Instantiate(DisplayedCards[i], new Vector3(_newX, _newY, 0f), Quaternion.identity);
        }
    }

    private void DisplayNext()
    {

    }

    private void DisplayPrev()
    {

    }

    private void ClearDisplayedCards()
    {
        if (DisplayedCards == null)
            return;

        for (int i = 0; i < 8; i++)
        {
            if (DisplayedCards[i] != null)
            {
                GameObject.Destroy(DisplayedCards[i], 0f);
                DisplayedCards[i] = null;
            }
        }
    }
}