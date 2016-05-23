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
        Debugger.LogPlayer(this, "summoning " + minionCard.Name + " at position " + position);

        // Creating a Minion and its Controller
        Minion minion = new Minion(minionCard);
        minion.Controller = MinionController.Create(BoardController, minion);

        // Adding the Minion to the Player Minion list
        Minions.Add(minion);

        // Adding the Minion to the BoardController
        BoardController.AddMinion(minion, position);

        // TODO : Fire events

        // Updating the Player glows
        UpdateSprites();
    }

    public void PlaySpell(SpellCard spellCard, Character target)
    {
        Debugger.LogPlayer(this, "starting to cast spell " + spellCard.Name + " to " + target);

        // Firing OnSpellPreCast events
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this, spellCard, target);

        if (target != spellPreCastEvent.Target)
        {
            Debugger.LogPlayer(this, " switching spell " + spellCard.Name + " target to " + target);

            // Re-setting the target because it has changed on the PreCast events
            target = spellPreCastEvent.Target;
        }

        // Checking if the Spell has not been cancelled
        if (spellPreCastEvent.Status != PreStatus.Cancelled)
        {
            Debugger.LogPlayer(this, "casting spell " + spellCard.Name + " to " + target);

            // Casting the Spell to the target
            spellCard.Cast(target);
        }
        else
        {
            Debugger.LogPlayer(this, "cancelled casting spell " + spellCard.Name);
        }

        // Firing OnSpellCasted events
        EventManager.Instance.OnSpellCasted(this, spellCard);

        // Adding the Spell to the Played Spells list
        UsedSpells.Add(spellCard);

        // Updating the Player glows
        UpdateSprites();
    }

    public void EquipWeapon(WeaponCard weaponCard)
    {
        Debugger.LogPlayer(this, "equipping weapon " + weaponCard.Name);

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
        UpdateSprites();
    }

    public void DestroyWeapon()
    {
        // Checking if the player has a Weapon
        if (Weapon != null)
        {
            Debugger.LogPlayer(this, "destroying weapon " + Weapon.Card.Name);

            // Firing the Weapon deathrattle
            Weapon.Card.Deathrattle();

            // Destroying the WeaponController
            Weapon.Controller.DestroyController();

            // TODO : Animation, sound, etc...

            // Setting the current Weapon as null
            Weapon = null;

            // Updating the Player glows
            UpdateSprites();
        }
    }

    public void UseHeroPower(Character target)
    {
        Debugger.LogPlayer(this, "using hero power " + Hero.HeroPower.Name + " to " + target.GetName());

        // Using the mana needed for the HeroPower
        UseMana(Hero.HeroPower.CurrentCost);

        // Using the HeroPower on the specified target
        Hero.HeroPower.Use(target);

        // Adding 1 to the current uses and updating the sprites
        Hero.HeroPower.CurrentUses++;
        Hero.HeroPower.Controller.UpdateSprites();

        // Firing OnInspired events
        EventManager.Instance.OnInspired(Hero, Hero.HeroPower);

        // Updating the Player glows
        UpdateSprites();
    }

    public void ReplaceHero(Hero newHero)
    {
        // TODO

        // Updating the Player glows
        UpdateSprites();
    }

    public void AddMana(int quantity)
    {
        AvailableMana = Mathf.Min(AvailableMana + quantity, MaximumMana);
        
        ManaController.UpdateAll();

        // Updating the Player glows
        UpdateSprites();
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
        UpdateSprites();
    }

    public void RefillMana()
    {
        AvailableMana = Mathf.Clamp(TurnMana - CurrentOverloadedMana, 0, 10);

        ManaController.UpdateAll();

        // Updating the Player glows
        UpdateSprites();
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
        UpdateSprites();
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
        UpdateSprites();
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
        UpdateSprites();
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
                // Drawing the first card in the deck
                BaseCard drawnBaseCard = DrawFromDeck(Deck[0]);
                
                return drawnBaseCard;
            }
            else
            {
                // TODO : Discard the card

                // Updating the Player glows
                UpdateSprites();
            }
        }
        else
        {
            // Suming 1 to the player fatigue
            Fatigue++;

            // Dealing fatigue damage to the Hero
            Hero.Damage(null, Fatigue);

            // Updating the Player glows
            UpdateSprites();
        }

        return null;
    }

    public BaseCard DrawFromDeck(BaseCard card)
    {
        if (Deck.Contains(card))
        {
            if (Hand.Count < MaxCardsInHand)
            {
                // Moving the card to the Hand
                Hand.Add(card);
                Deck.Remove(card);

                // Creating the visual controller for the card
                CardController cardController = CardController.Create(card);
                card.Controller = cardController;

                // Adding the Cardcontroller to the HandController
                HandController.Add(cardController);

                // Firing OnDrawn events
                card.OnDrawn();
                EventManager.Instance.OnCardDrawn(this, card);

                // Updating the Player glows
                UpdateSprites();

                return card;
            }
            else
            {
                // TODO : Discard the card

                // Updating the Player glows
                UpdateSprites();
            }
        }

        return null;
    }

    public void UpdateSprites()
    {
        ResetSprites();

        bool heroPowerUsable = Hero.HeroPower.IsUsable();
        
        Hero.HeroPower.Controller.CostController.SetEnabled(heroPowerUsable);
        Hero.HeroPower.Controller.FrontTokenRenderer.enabled = heroPowerUsable;
        Hero.HeroPower.Controller.BackTokenRenderer.enabled = !heroPowerUsable;

        bool isCurrentPlayer = GameManager.Instance.CurrentPlayer == this;
        
        if (HasWeapon())
        {
            Weapon.Controller.OpenTokenRenderer.enabled = isCurrentPlayer;
            Weapon.Controller.ClosedTokenRenderer.enabled = !isCurrentPlayer;
        }

        if (isCurrentPlayer)
        {
            UpdateHeroGlow();
            UpdateHandGlows();
            UpdateMinionGlows();
            UpdateHeroPowerGlow();
        }
    }

    public void ResetSprites()
    {
        HeroController.SetGreenRenderer(false);

        Hero.HeroPower.Controller.SetGreenRenderer(false);

        foreach (BaseCard card in Hand)
        {
            card.Controller.SetGreenRenderer(false);
        }

        foreach (Minion minion in Minions)
        {
            minion.Controller.SetGreenRenderer(false);
        }
    }

    private void UpdateHeroGlow()
    {
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
    }

    private void UpdateHandGlows()
    {
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
    }

    private void UpdateMinionGlows()
    {
        foreach (Minion minion in Minions)
        {
            if (minion.CanAttack())
            {
                minion.Controller.SetGreenRenderer(true);
            }
        }
    }

    private void UpdateHeroPowerGlow()
    {
        if (Hero.HeroPower.CurrentCost <= AvailableMana)
        {
            if (Hero.HeroPower.CurrentUses < Hero.HeroPower.MaxUses)
            {
                Hero.HeroPower.Controller.SetGreenRenderer(true);
            }
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