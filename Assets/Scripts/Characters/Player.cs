using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero;
    //public Deck Deck;
    //public List<Secret> Secrets;
    
    public int cardsInHand;
    public int maxCardsInHand; //maybe later add effects that increase it?
    public int currentMana;
    public int maxMana;
    public int availableMana;
    public int overloadedMana; //next turn overload
    public DeckManager deck;

    // set base values
    public void Init()
    {
        cardsInHand = 0;
        currentMana = 0;
        maxMana = 10;
        availableMana = 0;
        maxCardsInHand = 10;
        overloadedMana = 0;
    }

    public void RefillMana()
    {
        availableMana = currentMana - overloadedMana;
    }
}
