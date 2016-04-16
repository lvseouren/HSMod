using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();

    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>();
    public WeaponCard Weapon = null;
    
    public List<MinionCard> Minions = new List<MinionCard>(7);
    public List<SpellCard> Secrets = new List<SpellCard>();

    public GameObject DeckGameObject;

    public Player Enemy;

    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

    public int Fatigue = 0;

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
        OverloadedMana = 0;
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

    public int GetManaUsedThisTurn()
    {
        return TurnMana - OverloadedMana - AvailableMana;
    }

    public List<BaseCard> Draw(int draws)
    {
        List<BaseCard> drawnCards = new List<BaseCard>();

        for (int i = 0; i < draws; i++)
        {
            BaseCard drawnCard = Draw();

            if (drawnCard != null)
            {
                drawnCards.Add(drawnCard);
            }
        }

        return drawnCards;
    } 

    public BaseCard Draw()
    {
        if (Deck.Count > 0)
        {
            if (Hand.Count < MaxCardsInHand)
            {
                // Getting the first card in the Deck
                BaseCard drawnBaseCard = Deck[0];

                // Moving the card to the Hand
                Hand.Add(drawnBaseCard);
                Deck.Remove(drawnBaseCard);

                // Firing OnDrawn events
                drawnBaseCard.OnDrawn();
                EventManager.Instance.OnCardDrawn(this, drawnBaseCard);

                // TODO : GameObject stuff

                return drawnBaseCard;
            }
            else
            {
                // TODO : Discard the card

                return null;
            }
        }
        else
        {
            Fatigue++;

            this.Hero.TryDamage(null, Fatigue);

            return null;
        }
    }

    public void Equipweapon(WeaponCard weapon)
    {
        DestroyWeapon();

        Weapon = weapon;

        Weapon.Battlecry();
    }

    public void DestroyWeapon()
    {
        if (Weapon != null)
        {
            Weapon.Deathrattle();

            // TODO : Animation

            Weapon = null;
        }
    }

    public bool HasWeapon()
    {
        return (Weapon != null);
    }
}