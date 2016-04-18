using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero;

    public Player Enemy;

    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>();
    public List<MinionCard> Minions = new List<MinionCard>(7);
    public List<SpellCard> Secrets = new List<SpellCard>();
    public WeaponCard Weapon = null;

    public HeroController Controller;
    
    public List<SpellCard> UsedSpells = new List<SpellCard>();
    public List<MinionCard> DeadMinions = new List<MinionCard>();

    public Vector3 HeroPosition;
    public Vector3 CardsPosition;
    
    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

    public int Fatigue = 0;

    #region Constructor

    private Player() { }

    public static Player Create(Vector3 heroPosition, Vector3 cardsPosition)
    {
        Player player = new Player()
        {
            HeroPosition = heroPosition,
            CardsPosition = cardsPosition
        };

        player.Hero = Hero.Create(player);

        player.Initialize();

        return player;
    }

    private void Initialize()
    {
        this.Controller = HeroController.Create(this.Hero, HeroPosition);
    }

    #endregion

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

                // Creating the visual controller for the card
                drawnBaseCard.Controller = CardController.Create(drawnBaseCard);

                // Firing OnDrawn events
                drawnBaseCard.OnDrawn();
                EventManager.Instance.OnCardDrawn(this, drawnBaseCard);
                
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
            return null;
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

    public void UpdateGlows()
    {
        ResetGlows();

        if (this.HasWeapon() || this.Hero.CurrentAttack > 0)
        {
            switch (this.Hero.TurnAttacks)
            {
                case 0:
                    this.Controller.SetGreenRenderer(true);
                    break;

                case 1:
                    if (this.Weapon.Windfury)
                    {
                        this.Controller.SetGreenRenderer(true);
                    }
                    break;
            }
        }

        foreach (BaseCard card in this.Hand)
        {
            if (card.CurrentCost <= this.AvailableMana)
            {
                card.Controller.SetGreenRenderer(true);
            }
        }

        foreach (MinionCard minion in this.Minions)
        {
            if (minion.Frozen == false && minion.Sleeping == false)
            {
                if (minion.CanAttack())
                {
                    switch (minion.TurnAttacks)
                    {
                        case 0:
                            minion.Controller.SetGreenRenderer(true);
                            break;

                        case 1:
                            if (minion.Windfury)
                            {
                                minion.Controller.SetGreenRenderer(true);
                            }
                            break;
                    }
                }
            }
        }
    }

    public void ResetGlows()
    {
        this.Controller.SetGreenRenderer(false);

        foreach (BaseCard card in this.Hand)
        {
            card.Controller.SetGreenRenderer(false);
        }

        foreach (MinionCard minion in this.Minions)
        {
            minion.Controller.SetGreenRenderer(false);
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