using UnityEngine;
using System.Collections;

public class Player {
    // Put board effects like brann or rivendare here
    // as bools 
    // set true on card play, set false on silence/death/polymorph etc.
    private bool _rivendareDeathRattleEffect = false;
    //
    //
    
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
