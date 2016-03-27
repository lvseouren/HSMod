using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero;
    //public List<Secret> Secrets;
    public List<BaseCard> Hand;
    public Deck Deck;

    public int MaxCardsInHand; //maybe later add effects that increase it?
    public int CurrentMana;
    public int MaxMana;
    public int AvailableMana;
    public int OverloadedMana; //next turn overload

    public int GetHandSize()
    {
        return Hand.Count;
    }

    // set base values
    // TODO: add deck and hero
    public void Init()
    {
        //Secrets = new List<Secret>();
        Hand = new List<BaseCard>();
        CurrentMana = 0;
        MaxMana = 10;
        AvailableMana = 0;
        MaxCardsInHand = 10;
        OverloadedMana = 0;
    }

    public void RefillMana()
    {
        AvailableMana = CurrentMana - OverloadedMana;
    }
}
