using System.Reactive.Subjects;

public class EventManager
{
    // Singleton //
    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventManager();
            }
            return _instance;
        }
    }

    private EventManager() { }

    // Minion Event Subjects //
    public Subject<MinionPlayedEvent> MinionPlayedHandler = new Subject<MinionPlayedEvent>();

    public Subject<MinionPreAttackEvent> MinionPreAttackHandler = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> MinionAttackedHandler = new Subject<MinionAttackedEvent>();

    public Subject<MinionPreDamageEvent> MinionPreDamageHandler = new Subject<MinionPreDamageEvent>();
    public Subject<MinionDamagedEvent> MinionDamagedHandler = new Subject<MinionDamagedEvent>();

    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    // Hero Event Subjects //
    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackedEvent> HeroAttackedHandler = new Subject<HeroAttackedEvent>();

    public Subject<HeroPreDamageEvent> HeroPreDamageHandler = new Subject<HeroPreDamageEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();

    public Subject<HeroGainArmorEvent> HeroGainArmorHandler = new Subject<HeroGainArmorEvent>();

    // Spell Event Subjects //
    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastedEvent> SpellCastedHandler = new Subject<SpellCastedEvent>();

    // Card Event Subjects //
    public Subject<CardPlayedEvent> CardPlayedHandler = new Subject<CardPlayedEvent>();
    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();

    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

    // Weapon Event Subjects //
    public Subject<WeaponEquippedEvent> WeaponEquippedHandler = new Subject<WeaponEquippedEvent>();

    // Secret Event Subjects //
    public Subject<SecretPlayedEvent> SecretPlayedHandler = new Subject<SecretPlayedEvent>();
    public Subject<SecretRevealedEvent> SecretRevealedHandler = new Subject<SecretRevealedEvent>(); 

    public void Start()
    {
        // TODO : SETUP
    }

    #region Minion Event Handlers

    public void OnMinionPlayed(Player player, MinionCard minion)
    {
        MinionPlayedEvent minionPlayedEvent = new MinionPlayedEvent()
        {
            Player = player,
            Minion = minion
        };

        MinionPlayedHandler.OnNext(minionPlayedEvent);
    }

    public MinionPreAttackEvent OnMinionPreAttack(MinionCard minion, ICharacter target)
    {
        MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionPreAttackHandler.OnNext(minionPreAttackEvent);

        return minionPreAttackEvent;
    }

    public void OnMinionAttacked(MinionCard minion, ICharacter target)
    {
        MinionAttackedEvent minionAttackedEvent = new MinionAttackedEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionAttackedHandler.OnNext(minionAttackedEvent);
    }

    public MinionPreDamageEvent OnMinionPreDamage(ICharacter attacker, MinionCard minion)
    {
        MinionPreDamageEvent minionPreDamageEvent = new MinionPreDamageEvent()
        {
            Attacker = attacker,
            Minion = minion,
        };

        MinionPreDamageHandler.OnNext(minionPreDamageEvent);

        return minionPreDamageEvent;
    }

    public void OnMinionDamaged(ICharacter attacker, MinionCard minion)
    {
        MinionDamagedEvent minionDamagedEvent = new MinionDamagedEvent()
        {
            Attacker = attacker,
            Minion = minion
        };

        MinionDamagedHandler.OnNext(minionDamagedEvent);
    }

    public void OnMinionDied(MinionCard minion)
    {
        MinionDiedEvent minionDiedEvent = new MinionDiedEvent()
        {
            Minion = minion
        };

        MinionDiedHandler.OnNext(minionDiedEvent);
    }

    #endregion

    #region Hero Event Handlers

    public HeroPreAttackEvent OnHeroPreAttack(Hero hero, ICharacter target)
    {
        HeroPreAttackEvent heroPreAttackEvent = new HeroPreAttackEvent()
        {
            Hero = hero,
            Target = target
        };

        HeroPreAttackHandler.OnNext(heroPreAttackEvent);

        return heroPreAttackEvent;
    }

    public void OnHeroAttacked(Hero hero, ICharacter target)
    {
        HeroAttackedEvent heroAttackedEvent = new HeroAttackedEvent()
        {
            Hero = hero,
            Target = target
        };

        HeroAttackedHandler.OnNext(heroAttackedEvent);
    }

    public HeroPreDamageEvent OnHeroPreDamage(ICharacter attacker, Hero hero)
    {
        HeroPreDamageEvent heroPreDamageEvent = new HeroPreDamageEvent()
        {
            Attacker = attacker,
            Hero = hero
        };

        HeroPreDamageHandler.OnNext(heroPreDamageEvent);

        return heroPreDamageEvent;
    }

    public void OnHeroDamaged(ICharacter attacker, Hero hero, int damageAmount)
    {
        HeroDamagedEvent heroDamagedEvent = new HeroDamagedEvent()
        {
            Attacker = attacker,
            Hero = hero,
            Damage = damageAmount
        };

        HeroDamagedHandler.OnNext(heroDamagedEvent);
    }

    public void OnHeroGainArmor(Hero hero, int armorAmount)
    {
        HeroGainArmorEvent heroGainArmorEvent = new HeroGainArmorEvent()
        {
            Hero = hero,
            Armor = armorAmount
        };

        HeroGainArmorHandler.OnNext(heroGainArmorEvent);
    }

    #endregion

    #region Spell Event Handlers

    public SpellPreCastEvent OnSpellPreCast(Player player, SpellCard spell)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellPreCastEvent spellPreCastEvent = new SpellPreCastEvent()
        {
            Player = player,
            Spell = spell
        };

        SpellPreCastHandler.OnNext(spellPreCastEvent);

        return spellPreCastEvent;
    }

    public void OnSpellCasted(Hero hero, SpellCard spell)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellCastedEvent spellCastedEvent = new SpellCastedEvent()
        {
            Hero = hero,
            Spell = spell
        };

        SpellCastedHandler.OnNext(spellCastedEvent);
    }

    #endregion 

    #region Card Event Handlers

    public void OnCardPlayed(Player player, BaseCard card)
    {
        CardPlayedEvent cardPlayedEvent = new CardPlayedEvent()
        {
            Player = player,
            Card = card
        };

        CardPlayedHandler.OnNext(cardPlayedEvent);
    }

    public void OnCardDrawn(Player player, BaseCard card)
    {
        CardDrawnEvent cardDrawnEvent = new CardDrawnEvent()
        {
            Player = player,
            Card = card
        };

        CardDrawnHandler.OnNext(cardDrawnEvent);
    }

    public void OnCardDiscarded(Player player, BaseCard card)
    {
        CardDiscardedEvent cardDiscardedEvent = new CardDiscardedEvent()
        {
            Player = player,
            Card = card
        };

        CardDiscardedHandler.OnNext(cardDiscardedEvent);
    }

    #endregion

    #region Weapon Event Handlers

    public void OnWeaponEquipped(Player player, WeaponCard weapon)
    {
        WeaponEquippedEvent weaponEquippedEvent = new WeaponEquippedEvent()
        {
            Player = player,
            Weapon = weapon
        };

        WeaponEquippedHandler.OnNext(weaponEquippedEvent);
    }

    #endregion

    #region Secret Event Handlers

    public void OnSecretPlayed(Player player, SpellCard secret)
    {
        SecretPlayedEvent secretPlayedEvent = new SecretPlayedEvent()
        {
            Player = player,
            Secret = secret
        };

        SecretPlayedHandler.OnNext(secretPlayedEvent);
    }

    public void OnSecretRevealed(Player player, SpellCard secret)
    {
        SecretRevealedEvent secretRevealedEvent = new SecretRevealedEvent()
        {
            Player = player,
            Secret = secret
        };

        SecretRevealedHandler.OnNext(secretRevealedEvent);
    }

    #endregion
}