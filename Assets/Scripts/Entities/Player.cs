using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player Enemy;

    public Hero Hero;
    public Weapon Weapon;
    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Deck = new List<BaseCard>();
    public List<Minion> Minions = new List<Minion>(7);
    public List<SpellCard> Secrets = new List<SpellCard>();

    public ManaController ManaController;
    public HeroController HeroController;
    public HandController HandController;
    public BoardController BoardController;

    public List<SpellCard> UsedSpells = new List<SpellCard>();
    public List<MinionCard> DeadMinions = new List<MinionCard>();
    
    public int MaxCardsInHand = 10;
    public int MaxCardsInDeck = 60;

    public int Fatigue = 0;
    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int UsedMana = 0;
    public int AvailableMana;
    public int CurrentOverloadedMana = 0;
    public int NextOverloadedMana = 0;

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

    public void SummonMinion(MinionCard minionCard)
    {
        SummonMinion(minionCard, Minions.Count);
    }

    // TODO : Board positioning
    public void SummonMinion(MinionCard minionCard, int position)
    {
        // Creating a Minion and its Controller
        Minion minion = new Minion(minionCard);
        minion.Controller = MinionController.Create(BoardController, minion);

        // Adding the Minion to the Player Minion list
        Minions.Add(minion);

        // Adding the Minion to the BoardController
        BoardController.AddMinion(minion, position);

        // TODO : Fire events

        // Updating the Player glows
        UpdateGlows();
    }

    public void PlaySpell(SpellCard spellCard, Character target)
    {
        // Firing OnSpellPreCast events
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this, spellCard, target);

        // Re-setting the target, just in case it has changed on the PreCast events
        target = spellPreCastEvent.Target;

        // Checking if the Spell has not been cancelled
        if (spellPreCastEvent.Status != PreStatus.Cancelled)
        {
            // Casting the Spell to the target
            spellCard.Cast(target);
        }

        // Firing OnSpellCasted events
        EventManager.Instance.OnSpellCasted(this, spellCard);

        // Adding the Spell to the Played Spells list
        UsedSpells.Add(spellCard);
    }

    public void EquipWeapon(WeaponCard weaponCard)
    {
        // Destroying the previous Weapon
        DestroyWeapon();

        // Creating a Weapon and its Controller
        Weapon weapon = new Weapon(weaponCard);
        weapon.Controller = WeaponController.Create(this, weapon);

        // Setting the current Weapon as the new Weapon
        Weapon = weapon;

        // Firing the Weapon battlecry
        Weapon.Card.Battlecry();

        // Updating the Player glows
        UpdateGlows();
    }

    public void DestroyWeapon()
    {
        // Checking if the player has a Weapon
        if (Weapon != null)
        {
            // Firing the Weapon deathrattle
            Weapon.Card.Deathrattle();

            // TODO : Animation, sound, etc...

            // Setting the current Weapon as null
            Weapon = null;
        }

        // Updating the Player glows
        UpdateGlows();
    }

    public void ReplaceHero(Hero newHero)
    {
        // TODO
    }

    public void AddMana(int quantity)
    {
        AvailableMana = Mathf.Min(AvailableMana + quantity, MaximumMana);
        
        ManaController.UpdateAll();

        // Updating the Player glows
        UpdateGlows();
    }

    public void AddEmptyMana(int quantity)
    {
        TurnMana = Mathf.Min(TurnMana + quantity, MaximumMana);

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

        // Updating the Player glows
        UpdateGlows();
    }

    public void RefillMana()
    {
        AvailableMana = Mathf.Clamp(TurnMana - CurrentOverloadedMana, 0, 10);

        ManaController.UpdateAll();
    }

    // TODO : Animation
    public void AddCardToHand(BaseCard card)
    {
        if (Hand.Count < MaxCardsInHand)
        {
            card.Controller = CardController.Create(card);
            Hand.Add(card);
        }
        else
        {
            // TODO : Destroy card because hand is full
        }

        // Updating the Player glows
        UpdateGlows();
    }

    // TODO : Animation
    public void AddCardToDeck(BaseCard card)
    {
        if (Deck.Count < MaxCardsInDeck)
        {
            Deck.Add(card);
        }
        else
        {
            // TODO : Destroy card because deck is full (?)
        }

        // Updating the Player glows
        UpdateGlows();
    }

    public void RemoveCardFromHand(BaseCard card)
    {
        if (Hand.Contains(card))
        {
            // Removing the Card from the Hand
            Hand.Remove(card);
            HandController.Remove(card.Controller);

            // Destroying the CardController
            card.Controller.DestroyController();
        }

        // Updating the Player glows
        UpdateGlows();
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

                // Adding the Cardcontroller to the HandController
                HandController.Add(drawnCardController);

                // Firing OnDrawn events
                drawnBaseCard.OnDrawn();
                EventManager.Instance.OnCardDrawn(this, drawnBaseCard);

                // Updating the Player glows
                UpdateGlows();
                
                return drawnBaseCard;
            }
            else
            {
                // TODO : Discard the card

                // Updating the Player glows
                UpdateGlows();

                return null;
            }
        }
        else
        {
            Fatigue++;

            Hero.Damage(null, Fatigue);

            // Updating the Player glows
            UpdateGlows();

            return null;
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
                    if (Weapon.HasWindfury)
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
                if (card.GetCardType() == CardType.Minion)
                {
                    if (Minions.Count < 7)
                    {
                        card.Controller.SetGreenRenderer(true);
                    }
                }
                else
                {
                    card.Controller.SetGreenRenderer(true);
                }
            }
        }

        foreach (Minion minion in Minions)
        {
            if (minion.CanAttack())
            {
                minion.Controller.SetGreenRenderer(true);
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