using System.Collections.Generic;
using System.Linq;
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

    public Subject<HeroGainedArmorEvent> HeroGainedArmorHandler = new Subject<HeroGainedArmorEvent>();

    public Subject<HeroPowerEvent> HeroPowerHandler = new Subject<HeroPowerEvent>();

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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionPlayed.OnNext(minion);
        }
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionDamaged.OnNext(minion);
        }
    }

    public void OnMinionDied(MinionCard minion)
    {
        MinionDiedEvent minionDiedEvent = new MinionDiedEvent()
        {
            Minion = minion
        };

        MinionDiedHandler.OnNext(minionDiedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionDied.OnNext(minion);
        }
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroDamaged.OnNext(null);
        }
    }

    public void OnHeroGainedArmor(Hero hero, int armorAmount)
    {
        HeroGainedArmorEvent heroGainedArmorEvent = new HeroGainedArmorEvent()
        {
            Hero = hero,
            Armor = armorAmount
        };

        HeroGainedArmorHandler.OnNext(heroGainedArmorEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroGainedArmor.OnNext(null);
        }
    }

    public void OnHeroPower(Hero hero, HeroPower heroPower)
    {
        HeroPowerEvent heroPowerEvent = new HeroPowerEvent()
        {
            Hero = hero,
            HeroPower = heroPower
        };

        HeroPowerHandler.OnNext(heroPowerEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnInspired.OnNext(null);
        }
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnSpellPreCast.OnNext(spell);
        }

        return spellPreCastEvent;
    }

    public void OnSpellCasted(Player player, SpellCard spell)
    {
        // WARNING : May have problems here with the target being null for NoTarget spells
        SpellCastedEvent spellCastedEvent = new SpellCastedEvent()
        {
            Player = player,
            Spell = spell
        };

        SpellCastedHandler.OnNext(spellCastedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnSpellCasted.OnNext(spell);
        }
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            // TODO : Trigger overloaded/battlecry card played events
            battlefieldMinion.BuffManager.OnCardPlayed.OnNext(null);
        }
    }

    public void OnCardDrawn(Player player, BaseCard card)
    {
        CardDrawnEvent cardDrawnEvent = new CardDrawnEvent()
        {
            Player = player,
            Card = card
        };

        CardDrawnHandler.OnNext(cardDrawnEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnCardDrawn.OnNext(card);
        }
    }

    public void OnCardDiscarded(Player player, BaseCard card)
    {
        CardDiscardedEvent cardDiscardedEvent = new CardDiscardedEvent()
        {
            Player = player,
            Card = card
        };

        CardDiscardedHandler.OnNext(cardDiscardedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnCardDiscarded.OnNext(null);
        }
    }

    #endregion

    #region Weapon Event Handlers

    // TODO : Rename ?
    public void OnWeaponEquipped(Player player, WeaponCard weapon)
    {
        WeaponEquippedEvent weaponEquippedEvent = new WeaponEquippedEvent()
        {
            Player = player,
            Weapon = weapon
        };

        WeaponEquippedHandler.OnNext(weaponEquippedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroEquippedWeapon.OnNext(null);
        }
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