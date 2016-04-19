using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedDeck
{
    public string Name;
    public HeroClass Class;

    public SavedCard[] SavedCards;

    public SavedDeck(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public List<BaseCard> ToGameDeck()
    {
        List<BaseCard> gameDeck = new List<BaseCard>();

        foreach (SavedCard card in SavedCards)
        {
            gameDeck.Add(card.ToGameCard());
        }

        return gameDeck;
    }
}