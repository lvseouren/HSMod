﻿using UnityEngine;

public abstract class BaseCard : MonoBehaviour
{
    // Base Stats //
    public string Name;
    public string Description;
    public int BaseCost;
    public CardClass CardClass;
    public Rarity Rarity;
    public bool Golden = false;

    // In-Game Stats //
    public Player Player;
    public int CurrentCost;

    // Effects //
    public int Overload = 0;
    public bool Combo = false;

    // TODO : Image, Voices, Effects?

    #region Hand/Deck Events

    public virtual void OnDrawn()
    {
        
    }

    public virtual void OnDiscarded()
    {
        
    }

    #endregion
}

public enum CardClass
{
    Neutral,
    Druid,
    Hunter,
    Mage,
    Paladin,
    Priest,
    Rogue,
    Shaman,
    Warlock,
    Warrior,
    DeathKnight,
    Monk,
    DemonHunter
}

public enum Rarity
{
    Basic,
    Common,
    Rare,
    Epic,
    Legendary
}