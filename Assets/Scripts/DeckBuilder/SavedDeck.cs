using System;
using UnityEngine;

[Serializable]
public class SavedDeck
{
    public string Name;
    public CardClass Class;

    public SavedCard[] SavedCards;

    public SavedDeck(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
}