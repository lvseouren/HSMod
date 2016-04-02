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
    public Subject<MinionAttackEvent> MinionAttackHandler = new Subject<MinionAttackEvent>();
    public Subject<MinionPreDamagedEvent> MinionPreDamagedHandler = new Subject<MinionPreDamagedEvent>();
    public Subject<MinionDamagedEvent> MinionDamagedHandler = new Subject<MinionDamagedEvent>();
    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    // Hero Event Subjects //
    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackEvent> HeroAttackHandler = new Subject<HeroAttackEvent>();
    public Subject<HeroPreDamagedEvent> HeroPreDamagedHandler = new Subject<HeroPreDamagedEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();
    public Subject<HeroGainArmorEvent> HeroGainArmorHandler = new Subject<HeroGainArmorEvent>();

    // Spell Event Subjects //
    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastEvent> SpellCastHandler = new Subject<SpellCastEvent>();

    // Card Event Subjects //
    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();
    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

    // Weapon Event Subjects //
    public Subject<WeaponEquipEvent> WeaponEquipHandler = new Subject<WeaponEquipEvent>();

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

    public bool OnMinionPreAttack(MinionCard minion, ICharacter target)
    {
        MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionPreAttackHandler.OnNext(minionPreAttackEvent);

        return minionPreAttackEvent.IsCancelled;
    }

    public void OnMinionAttack(MinionCard minion, ICharacter target, int damageAmount)
    {
        MinionAttackEvent minionAttackEvent = new MinionAttackEvent()
        {
            Minion = minion,
            Target = target,
            Damage = damageAmount
        };

        MinionAttackHandler.OnNext(minionAttackEvent);
    }

    public bool OnMinionPreDamaged(ICharacter attacker, MinionCard minion)
    {
        MinionPreDamagedEvent minionPreDamagedEvent = new MinionPreDamagedEvent()
        {
            Attacker = attacker,
            Minion = minion,
        };

        MinionPreDamagedHandler.OnNext(minionPreDamagedEvent);

        return minionPreDamagedEvent.IsCancelled;
    }

    public void OnMinionDamaged(ICharacter attacker, MinionCard minion, int damage)
    {
        MinionDamagedEvent minionDamagedEvent = new MinionDamagedEvent()
        {
            Attacker = attacker,
            Minion = minion,
            Damage = damage
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

    public bool OnHeroPreAttack(Hero hero, ICharacter target)
    {
        HeroPreAttackEvent heroPreAttackEvent = new HeroPreAttackEvent()
        {
            Hero = hero,
            Target = target
        };

        HeroPreAttackHandler.OnNext(heroPreAttackEvent);

        return heroPreAttackEvent.IsCancelled;
    }

    public void OnHeroAttack(Hero hero, ICharacter target, int damageAmount)
    {
        HeroAttackEvent heroAttackEvent = new HeroAttackEvent()
        {
            Hero = hero,
            Target = target,
            Damage = damageAmount
        };

        HeroAttackHandler.OnNext(heroAttackEvent);
    }

    public bool OnHeroPreDamaged(ICharacter attacker, Hero hero)
    {
        HeroPreDamagedEvent heroPreDamagedEvent = new HeroPreDamagedEvent()
        {
            Attacker = attacker,
            Hero = hero
        };

        HeroPreDamagedHandler.OnNext(heroPreDamagedEvent);

        return heroPreDamagedEvent.IsCancelled;
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

    public bool OnSpellPreCast(Hero hero, SpellCard spell)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellPreCastEvent spellPreCastEvent = new SpellPreCastEvent()
        {
            Hero = hero,
            Spell = spell
        };

        SpellPreCastHandler.OnNext(spellPreCastEvent);

        return spellPreCastEvent.IsCancelled;
    }

    public void OnSpellCast(Hero hero, SpellCard spell)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellCastEvent spellCastEvent = new SpellCastEvent()
        {
            Hero = hero,
            Spell = spell
        };

        SpellCastHandler.OnNext(spellCastEvent);
    }

    #endregion 

    #region Card Event Handlers

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

    public void OnWeaponEquip(Player player, WeaponCard weapon)
    {
        WeaponEquipEvent weaponEquipEvent = new WeaponEquipEvent()
        {
            Player = player,
            Weapon = weapon
        };

        WeaponEquipHandler.OnNext(weaponEquipEvent);
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