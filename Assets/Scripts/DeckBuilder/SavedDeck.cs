using System;
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
}