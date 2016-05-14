using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Hero Hero;

    public Player Enemy;

    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>();
    public List<Minion> Minions = new List<Minion>(7);
    public List<SpellCard> Secrets = new List<SpellCard>();
    public WeaponCard Weapon;

    public ManaController ManaController;
    public HeroController HeroController;
    public HandController HandController;
    public BoardController BoardController;
    
    public List<SpellCard> UsedSpells = new List<SpellCard>();
    public List<MinionCard> DeadMinions = new List<MinionCard>();
    
    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int Fatigue;

    public int MaximumMana = 10;

    public int TurnMana = 1; // Testing purposes - switch back to 0 when finished
    public int AvailableMana;
    public int UsedMana;
    public int CurrentOverloadedMana;
    public int NextOverloadedMana;

    #region Constructor

    private Player() { }

    public static Player Create(PlayerParameters parameters)
    {
        GameObject playerObject = new GameObject("Player_" + parameters.HeroClass.Name());
        playerObject.transform.localScale = Vector3.one * 50f;
        playerObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        playerObject.transform.position = parameters.PlayerPosition;

        Player player = playerObject.AddComponent<Player>();
        
        player.Deck = parameters.Deck;

        foreach (BaseCard card in player.Deck)
        {
            card.Player = player;
        }

        player.ManaController = ManaController.Create(player, parameters.ManaPosition, parameters.DisplayCrystals);
        player.HandController = HandController.Create(player, parameters.HandPosition, parameters.HandInverted);
        player.BoardController = BoardController.Create(player, parameters.BoardPosition);

        player.Hero = new Hero()
        {
            Player = player,
            Class = parameters.HeroClass,
            BaseHealth = parameters.HeroHealth,
            BaseArmor = parameters.HeroArmor
        };

        player.Hero.Initialize();

        player.HeroController = HeroController.Create(player.Hero);

        player.Hero.HeroPower = (BaseHeroPower) Activator.CreateInstance(parameters.HeroPower, new object[] { player.Hero });

        return player;
    }

    #endregion

    #region Methods

    public void PlayMinion(MinionCard minionCard, int position)
    {
        Minion minion = new Minion(minionCard);
        MinionController minionController = MinionController.Create(BoardController, minion);

        BoardController.UpdateSlots();

        HandController.Remove(minionCard.Controller);

        Hand.Remove(minionCard);
    }

    public void ReplaceHero(Hero newHero)
    {
        // TODO
    }

    public void AddMana(int quantity)
    {
        AvailableMana += quantity;

        if (AvailableMana > MaximumMana)
        {
            AvailableMana = MaximumMana;
        }
        
        ManaController.UpdateAll();
    }

    public void AddEmptyMana(int quantity)
    {
        TurnMana += quantity;

        if (TurnMana > MaximumMana)
        {
            TurnMana = MaximumMana;
        }

        ManaController.UpdateAll();
    }

    public void AddOverloadedMana(int quantity)
    {
        NextOverloadedMana += quantity;

        ManaController.UpdateAll();
    }

    public void UseMana(int quantity)
    {
        AvailableMana -= quantity;
        UsedMana += quantity;

        ManaController.UpdateAll();
    }

    public void RefillMana()
    {
        AvailableMana = Mathf.Clamp(TurnMana - CurrentOverloadedMana, 0, 10);

        ManaController.UpdateAll();
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

                HandController.Add(drawnCardController);

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
            Fatigue++;

            Hero.TryDamage(null, Fatigue);

            return null;
        }
    }

    public void EquipWeapon(WeaponCard weapon)
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

    public void UpdateGlows()
    {
        ResetGreenGlows();

        if (HasWeapon() || Hero.CurrentAttack > 0)
        {
            switch (Hero.CurrentTurnAttacks)
            {
                case 0:
                    HeroController.SetGreenRenderer(true);
                    break;

                case 1:
                    if (Weapon.Windfury)
                    {
                        HeroController.SetGreenRenderer(true);
                    }
                    break;
            }
        }

        foreach (BaseCard card in Hand)
        {
            if (card.CurrentCost <= AvailableMana)
            {
                card.Controller.SetGreenRenderer(true);
            }
        }

        foreach (Minion minion in Minions)
        {
            if (minion.IsFrozen == false && minion.IsSleeping == false)
            {
                if (minion.CanAttack())
                {
                    switch (minion.CurrentTurnAttacks)
                    {
                        case 0:
                            minion.Controller.SetGreenRenderer(true);
                            break;

                        case 1:
                            if (minion.HasWindfury)
                            {
                                minion.Controller.SetGreenRenderer(true);
                            }
                            break;
                    }
                }
            }
        }
    }

    public void ResetGreenGlows()
    {
        HeroController.SetGreenRenderer(false);

        foreach (BaseCard card in Hand)
        {
            card.Controller.SetGreenRenderer(false);
        }

        foreach (Minion minion in Minions)
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

        foreach (Minion minion in Minions)
        {
            spellPower += minion.SpellPower;
        }

        return spellPower;
    }

    public int GetUsedMana()
    {
        return TurnMana - CurrentOverloadedMana - AvailableMana;
    }

    #endregion

    #region Condition Checkers

    public bool HasWeapon()
    {
        return (Weapon != null);
    }

    public bool HasMinions()
    {
        return (Minions.Count > 0);
    }

    public bool HasTauntMinions()
    {
        foreach (Minion minion in Minions)
        {
            if (minion.HasTaunt)
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}