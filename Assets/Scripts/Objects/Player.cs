﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();
    public Hand Hand = new Hand();
    public Deck Deck = new Deck();
    public List<MinionCard> Minions = new List<MinionCard>(7);
    public List<SpellCard> Secrets;

    public GameObject DeckGameObject;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

    public int SpellDamage = 0;

    public void Start()
    {
        
    }

    public void ReplaceHero(Hero newHero)
    {
        // TODO
    }

    public void RefillMana()
    {
        AvailableMana = TurnMana - OverloadedMana;
    }
}