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

    // Spell Event Subjects //
    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastEvent> SpellCastHandler = new Subject<SpellCastEvent>();

    // Card Event Subjects //
    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();
    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

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

    #endregion

    #region Spell Event Handlers

    public bool OnSpellPreCast(Hero hero, SpellCard spell, ICharacter target)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellPreCastEvent spellPreCastEvent = new SpellPreCastEvent()
        {
            Hero = hero,
            Spell = spell,
            Target = target
        };

        SpellPreCastHandler.OnNext(spellPreCastEvent);

        return spellPreCastEvent.IsCancelled;
    }

    public void OnSpellCast(Hero hero, SpellCard spell, ICharacter target)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellCastEvent spellCastEvent = new SpellCastEvent()
        {
            Hero = hero,
            Spell = spell,
            Target = target
        };

        SpellCastHandler.OnNext(spellCastEvent);
    }

    #endregion
}