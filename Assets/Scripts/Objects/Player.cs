using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();
    //public List<Secret> Secrets;
    public List<BaseCard> Hand = new List<BaseCard>();
    public Deck Deck;

    public int MaxCardsInHand; //maybe later add effects that increase it?

    public int CurrentMana = 0;
    public int MaxMana = 10;
    public int AvailableMana = 0;
    public int OverloadedMana = 0;

    public int SpellDamage = 0;

    public void Start()
    {
        
    }

    public int GetHandSize()
    {
        return Hand.Count;
    }

    public void RefillMana()
    {
        AvailableMana = CurrentMana - OverloadedMana;
    }
}
