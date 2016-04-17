using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero = new Hero();

    public Player Enemy;

    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>();
    public List<MinionCard> Minions = new List<MinionCard>(7);
    public List<SpellCard> Secrets = new List<SpellCard>();
    public WeaponCard Weapon = null;

    public HeroController Controller;

    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

    public int Fatigue = 0;

    public void Start()
    {
        this.Controller = HeroController.Create(this.Hero);
    }

    #region Methods

    public void ReplaceHero(Hero newHero)
    {
        // TODO
    }

    public void RefillMana()
    {
        AvailableMana = TurnMana - OverloadedMana;
        OverloadedMana = 0;
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

    public void EquipWeapon(WeaponCard weapon)
    {
        DestroyWeapon();

        this.Weapon = weapon;

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

    #endregion

    #region Getter Methods
    
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

    #endregion

    #region Condition Checkers

    public bool HasWeapon()
    {
        return (Weapon != null);
    }

    public bool HasMinions()
    {
        return (this.Minions.Count > 0);
    }

    public bool HasTauntMinions()
    {
        foreach (MinionCard minion in this.Minions)
        {
            if (minion.Taunt)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

}