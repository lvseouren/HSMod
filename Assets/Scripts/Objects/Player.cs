using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();
    public Hand Hand = new Hand();
    public Deck Deck = new Deck();

    public GameObject DeckGameObject;

    public List<SpellCard> Secrets;

    public int CurrentMana = 0;
    public int MaximumMana = 10;
    public int AvailableMana = 0;
    public int OverloadedMana = 0;

    public int SpellDamage = 0;

    public void Start()
    {
        
    }

    public void RefillMana()
    {
        AvailableMana = CurrentMana - OverloadedMana;
    }
}