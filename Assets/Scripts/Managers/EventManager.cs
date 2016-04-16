﻿using System.Reactive.Subjects;

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

    public Subject<MinionPreHealEvent> MinionPreHealHandler = new Subject<MinionPreHealEvent>();
    public Subject<MinionHealedEvent> MinionHealedHandler = new Subject<MinionHealedEvent>();

    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    // Hero Event Subjects //
    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackedEvent> HeroAttackedHandler = new Subject<HeroAttackedEvent>();

    public Subject<HeroPreDamageEvent> HeroPreDamageHandler = new Subject<HeroPreDamageEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();

    public Subject<HeroPreHealEvent> HeroPreHealHandler = new Subject<HeroPreHealEvent>();
    public Subject<HeroHealedEvent> HeroHealedHandler = new Subject<HeroHealedEvent>();

    public Subject<HeroGainedArmorEvent> HeroGainedArmorHandler = new Subject<HeroGainedArmorEvent>();

    public Subject<HeroEquippedWeaponEvent> HeroEquippedWeaponHandler = new Subject<HeroEquippedWeaponEvent>();

    public Subject<HeroPowerEvent> HeroPowerHandler = new Subject<HeroPowerEvent>();

    // Spell Event Subjects //
    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastedEvent> SpellCastedHandler = new Subject<SpellCastedEvent>();

    // Card Event Subjects //
    public Subject<CardPlayedEvent> CardPlayedHandler = new Subject<CardPlayedEvent>();
    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();

    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

    // Secret Event Subjects //
    public Subject<SecretPlayedEvent> SecretPlayedHandler = new Subject<SecretPlayedEvent>();
    public Subject<SecretRevealedEvent> SecretRevealedHandler = new Subject<SecretRevealedEvent>();

    // Turn Event Subjects //
    public Subject<TurnEvent> TurnStartHandler = new Subject<TurnEvent>();
    public Subject<TurnEvent> TurnEndHandler = new Subject<TurnEvent>();

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
            battlefieldMinion.BuffManager.OnMinionPlayed.OnNext(minionPlayedEvent);
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionPreAttack.OnNext(minionPreAttackEvent);
        }

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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionAttacked.OnNext(minionAttackedEvent);
        }
    }

    public MinionPreDamageEvent OnMinionPreDamage(MinionCard minion, ICharacter attacker, int damageAmount)
    {
        MinionPreDamageEvent minionPreDamageEvent = new MinionPreDamageEvent()
        {
            Minion = minion,
            Attacker = attacker,
            Damage = damageAmount
        };

        MinionPreDamageHandler.OnNext(minionPreDamageEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionPreDamage.OnNext(minionPreDamageEvent);
        }

        return minionPreDamageEvent;
    }

    public void OnMinionDamaged(MinionCard minion, ICharacter attacker, int damageAmount)
    {
        MinionDamagedEvent minionDamagedEvent = new MinionDamagedEvent()
        {
            Minion = minion,
            Attacker = attacker,
            Damage = damageAmount
        };

        MinionDamagedHandler.OnNext(minionDamagedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionDamaged.OnNext(minionDamagedEvent);
        }
    }

    public MinionPreHealEvent OnMinionPreHeal(MinionCard minion, int healAmount)
    {
        MinionPreHealEvent minionPreHealEvent = new MinionPreHealEvent()
        {
            Minion = minion,
            HealAmount = healAmount
        };

        MinionPreHealHandler.OnNext(minionPreHealEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionPreHeal.OnNext(minionPreHealEvent);
        }

        return minionPreHealEvent;
    }

    public void OnMinionHealed(MinionCard minion, int healAmount)
    {
        MinionHealedEvent minionHealedEvent = new MinionHealedEvent()
        {
            Minion = minion,
            HealAmount = healAmount
        };

        MinionHealedHandler.OnNext(minionHealedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnMinionHealed.OnNext(minionHealedEvent);
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
            battlefieldMinion.BuffManager.OnMinionDied.OnNext(minionDiedEvent);
        }
    }

    #endregion

    #region Hero Event Handlers

    public HeroPreAttackEvent OnHeroPreAttack(Hero hero, ICharacter target, int damageAmount)
    {
        HeroPreAttackEvent heroPreAttackEvent = new HeroPreAttackEvent()
        {
            Hero = hero,
            Target = target,
            Damage = damageAmount
        };

        HeroPreAttackHandler.OnNext(heroPreAttackEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroPreAttack.OnNext(heroPreAttackEvent);
        }

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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroAttacked.OnNext(heroAttackedEvent);
        }
    }

    public HeroPreDamageEvent OnHeroPreDamage(Hero hero, ICharacter attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = new HeroPreDamageEvent()
        {
            Hero = hero,
            Attacker = attacker,
            Damage = damageAmount
        };

        HeroPreDamageHandler.OnNext(heroPreDamageEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroPreDamage.OnNext(heroPreDamageEvent);
        }

        return heroPreDamageEvent;
    }

    public void OnHeroDamaged(Hero hero, ICharacter attacker, int damageAmount)
    {
        HeroDamagedEvent heroDamagedEvent = new HeroDamagedEvent()
        {
            Hero = hero,
            Attacker = attacker,
            Damage = damageAmount
        };

        HeroDamagedHandler.OnNext(heroDamagedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroDamaged.OnNext(heroDamagedEvent);
        }
    }

    public HeroPreHealEvent OnHeroPreHeal(Hero hero, int healAmount)
    {
        HeroPreHealEvent heroPreHealEvent = new HeroPreHealEvent()
        {
            Hero = hero,
            HealAmount = healAmount
        };

        HeroPreHealHandler.OnNext(heroPreHealEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroPreHeal.OnNext(heroPreHealEvent);
        }

        return heroPreHealEvent;
    }

    public void OnHeroHealed(Hero hero, int healAmount)
    {
        HeroHealedEvent heroHealedEvent = new HeroHealedEvent()
        {
            Hero = hero,
            HealAmount = healAmount
        };

        HeroHealedHandler.OnNext(heroHealedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroHealed.OnNext(heroHealedEvent);
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
            battlefieldMinion.BuffManager.OnHeroGainedArmor.OnNext(heroGainedArmorEvent);
        }
    }

    public void OnHeroEquippedWeapon(Player player, WeaponCard weapon)
    {
        HeroEquippedWeaponEvent heroEquippedWeaponEvent = new HeroEquippedWeaponEvent()
        {
            Player = player,
            Weapon = weapon
        };

        HeroEquippedWeaponHandler.OnNext(heroEquippedWeaponEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnHeroEquippedWeapon.OnNext(heroEquippedWeaponEvent);
        }
    }

    public void OnHeroPower(Hero hero, BaseHeroPower heroPower)
    {
        HeroPowerEvent heroPowerEvent = new HeroPowerEvent()
        {
            Hero = hero,
            HeroPower = heroPower
        };

        HeroPowerHandler.OnNext(heroPowerEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnInspired.OnNext(heroPowerEvent);
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
            battlefieldMinion.BuffManager.OnCardPlayed.OnNext(cardPlayedEvent);
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
            battlefieldMinion.BuffManager.OnCardDrawn.OnNext(cardDrawnEvent);
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
            battlefieldMinion.BuffManager.OnCardDiscarded.OnNext(cardDiscardedEvent);
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

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnSecretPlayed.OnNext(secretPlayedEvent);
        }
    }

    public void OnSecretRevealed(Player player, SpellCard secret)
    {
        SecretRevealedEvent secretRevealedEvent = new SecretRevealedEvent()
        {
            Player = player,
            Secret = secret
        };

        SecretRevealedHandler.OnNext(secretRevealedEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnSecretRevealed.OnNext(secretRevealedEvent);
        }
    }

    #endregion

    #region Turn Event Handlers

    public void OnTurnStart(Player player)
    {
        TurnEvent turnStartEvent = new TurnEvent()
        {
            Player = player
        };

        TurnStartHandler.OnNext(turnStartEvent);

        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnTurnStart.OnNext(turnStartEvent);
        }
    }

    public void OnTurnEnd(Player player)
    {
        TurnEvent turnEndEvent = new TurnEvent()
        {
            Player = player
        };

        TurnStartHandler.OnNext(turnEndEvent);
        
        foreach (MinionCard battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.BuffManager.OnTurnEnd.OnNext(turnEndEvent);
        }
    }

    #endregion
}