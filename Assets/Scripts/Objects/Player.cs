using System.Collections.Generic;
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

    public HeroController HeroController;
    public HandController HandController;
    
    public List<SpellCard> UsedSpells = new List<SpellCard>();
    public List<MinionCard> DeadMinions = new List<MinionCard>();

    public Vector3 HeroPosition;
    public Vector3 HandPosition;
    
    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana = 0;

    public int Fatigue = 0;

    #region Constructor

    private Player() { }

    public static Player Create(HeroClass heroClass, Vector3 heroPosition, Vector3 cardsPosition)
    {
        Player player = new Player()
        {
            HeroPosition = heroPosition,
            HandPosition = cardsPosition
        };

        player.Hero = Hero.Create(player, heroClass);

        player.Initialize();

        return player;
    }

    private void Initialize()
    {
        this.HeroController = HeroController.Create(this.Hero, HeroPosition);
        this.HandController = HandController.Create(this, HandPosition);
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
                CardController drawnCardController = CardController.Create(drawnBaseCard);
                drawnBaseCard.Controller = drawnCardController;

                this.HandController.Add(drawnCardController);

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
                    this.HeroController.SetGreenRenderer(true);
                    break;

                case 1:
                    if (this.Weapon.Windfury)
                    {
                        this.HeroController.SetGreenRenderer(true);
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
        this.HeroController.SetGreenRenderer(false);

        foreach (BaseCard card in this.Hand)
        {
            card.Controller.SetGreenRenderer(false);
        }

        foreach (MinionCard minion in this.Minions)
        {
            minion.Controller.SetGreenRenderer(false);
        }
    }

    public void UpdateAll()
    {
        HeroController.UpdateSprites();
        HeroController.UpdateNumbers();

        if (HasWeapon())
        {
            Weapon.Controller.UpdateSprites();
            Weapon.Controller.UpdateNumbers();
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