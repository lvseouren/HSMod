using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();

    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>(); 

    public List<MinionCard> Minions = new List<MinionCard>(7);
    public List<SpellCard> Secrets;

    public GameObject DeckGameObject;

    public Player Enemy;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

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

    public int GetSpellPower()
    {
        int spellPower = 0;

        foreach (MinionCard minion in Minions)
        {
            spellPower += minion.SpellPower;
        }

        return spellPower;
    }
}   