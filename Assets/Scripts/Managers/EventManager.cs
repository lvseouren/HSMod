using System.Reactive.Subjects;

public class EventManager
{
    #region Singleton
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
    #endregion

    #region Subjects

    // Minion Event Subjects //
    public Subject<MinionPlayedEvent> MinionPlayedHandler = new Subject<MinionPlayedEvent>();

    public Subject<MinionPreAttackEvent> MinionPreAttackHandler = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> MinionAttackedHandler = new Subject<MinionAttackedEvent>();

    public Subject<MinionPreDamageEvent> MinionPreDamageHandler = new Subject<MinionPreDamageEvent>();
    public Subject<MinionDamagedEvent> MinionDamagedHandler = new Subject<MinionDamagedEvent>();

    public Subject<MinionPreHealEvent> MinionPreHealHandler = new Subject<MinionPreHealEvent>();
    public Subject<MinionHealedEvent> MinionHealedHandler = new Subject<MinionHealedEvent>();

    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    public Subject<MinionPoisonedEvent> MinionPoisonedHandler = new Subject<MinionPoisonedEvent>();

    public Subject<MinionEnragedEvent> MinionEnragedHandler = new Subject<MinionEnragedEvent>();

    public Subject<MinionFrozenEvent> MinionFrozenHandler = new Subject<MinionFrozenEvent>();

    // Hero Event Subjects //
    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackedEvent> HeroAttackedHandler = new Subject<HeroAttackedEvent>();

    public Subject<HeroPreDamageEvent> HeroPreDamageHandler = new Subject<HeroPreDamageEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();

    public Subject<HeroPreHealEvent> HeroPreHealHandler = new Subject<HeroPreHealEvent>();
    public Subject<HeroHealedEvent> HeroHealedHandler = new Subject<HeroHealedEvent>();

    public Subject<HeroGainedArmorEvent> HeroGainedArmorHandler = new Subject<HeroGainedArmorEvent>();

    public Subject<HeroEquippedWeaponEvent> HeroEquippedWeaponHandler = new Subject<HeroEquippedWeaponEvent>();

    public Subject<InspireEvent> InspireHandler = new Subject<InspireEvent>();

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

    #endregion

    #region Handlers

    public void OnMinionFrozen(Minion minion, Character freezingMinion)
    {
        MinionFrozenEvent minionFrozenEvent = new MinionFrozenEvent()
        {
            Minion = minion,
            FreezingCharacter = freezingMinion
        };

        MinionFrozenHandler.OnNext(minionFrozenEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionFrozen.OnNext(minionFrozenEvent);
        }
    }

    public void OnMinionEnraged(Minion minion, Character enragedMinion)
    {
        MinionEnragedEvent minionEnragedEvent = new MinionEnragedEvent()
        {
            Minion = minion,
            EnragedCharacter = enragedMinion
        };

        MinionEnragedHandler.OnNext(minionEnragedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionEnraged.OnNext(minionEnragedEvent);
        }
    }

    public void OnMinionEnrageded(Minion minion, Character enragedMinion)
    {
        MinionEnragedEvent minionEnragedEvent = new MinionEnragedEvent()
        {
            Minion = minion,
            EnragedCharacter = enragedMinion
        };

        MinionEnragedHandler.OnNext(minionEnragedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionEnraged.OnNext(minionEnragedEvent);
        }
    }

    public void OnMinionPoisoned(Minion minion, Character attacker)
    {
        MinionPoisonedEvent minionPoisonedEvent = new MinionPoisonedEvent()
        {
            Minion = minion,
            Attacker = attacker
        };

        MinionPoisonedHandler.OnNext(minionPoisonedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionPoisoned.OnNext(minionPoisonedEvent);
        }

    }

    public void OnMinionPlayed(Player player, Minion minion)
    {
        MinionPlayedEvent minionPlayedEvent = new MinionPlayedEvent()
        {
            Player = player,
            Minion = minion
        };

        MinionPlayedHandler.OnNext(minionPlayedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionPlayed.OnNext(minionPlayedEvent);
        }
    }

    public MinionPreAttackEvent OnMinionPreAttack(Minion minion, Character target)
    {
        MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionPreAttackHandler.OnNext(minionPreAttackEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionPreAttack.OnNext(minionPreAttackEvent);
        }

        return minionPreAttackEvent;
    }

    public void OnMinionAttacked(Minion minion, Character target)
    {
        MinionAttackedEvent minionAttackedEvent = new MinionAttackedEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionAttackedHandler.OnNext(minionAttackedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionAttacked.OnNext(minionAttackedEvent);
        }
    }

    public MinionPreDamageEvent OnMinionPreDamage(MinionPreDamageEvent minionPreDamageEvent)
    {
        MinionPreDamageHandler.OnNext(minionPreDamageEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionPreDamage.OnNext(minionPreDamageEvent);
        }

        return minionPreDamageEvent;
    }

    public void OnMinionDamaged(MinionDamagedEvent minionDamagedEvent)
    {
        MinionDamagedHandler.OnNext(minionDamagedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionDamaged.OnNext(minionDamagedEvent);
        }
    }

    public MinionPreHealEvent OnMinionPreHeal(Minion minion, int healAmount)
    {
        MinionPreHealEvent minionPreHealEvent = new MinionPreHealEvent()
        {
            Minion = minion,
            HealAmount = healAmount
        };

        MinionPreHealHandler.OnNext(minionPreHealEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionPreHeal.OnNext(minionPreHealEvent);
        }

        return minionPreHealEvent;
    }

    public void OnMinionHealed(Minion minion, int healAmount)
    {
        MinionHealedEvent minionHealedEvent = new MinionHealedEvent()
        {
            Minion = minion,
            HealAmount = healAmount
        };

        MinionHealedHandler.OnNext(minionHealedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionHealed.OnNext(minionHealedEvent);
        }
    }

    public void OnMinionDied(Minion minion)
    {
        MinionDiedEvent minionDiedEvent = new MinionDiedEvent()
        {
            Minion = minion
        };

        MinionDiedHandler.OnNext(minionDiedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnMinionDied.OnNext(minionDiedEvent);
        }
    }

    public HeroPreAttackEvent OnHeroPreAttack(Hero hero, Character target)
    {
        HeroPreAttackEvent heroPreAttackEvent = new HeroPreAttackEvent()
        {
            Hero = hero,
            Target = target
        };

        HeroPreAttackHandler.OnNext(heroPreAttackEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroPreAttack.OnNext(heroPreAttackEvent);
        }

        return heroPreAttackEvent;
    }

    public void OnHeroAttacked(Hero hero, Character target)
    {
        HeroAttackedEvent heroAttackedEvent = new HeroAttackedEvent()
        {
            Hero = hero,
            Target = target
        };

        HeroAttackedHandler.OnNext(heroAttackedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroAttacked.OnNext(heroAttackedEvent);
        }
    }

    public HeroPreDamageEvent OnHeroPreDamage(Hero hero, Character attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = new HeroPreDamageEvent()
        {
            Hero = hero,
            Attacker = attacker,
            DamageAmount = damageAmount
        };

        HeroPreDamageHandler.OnNext(heroPreDamageEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroPreDamage.OnNext(heroPreDamageEvent);
        }

        return heroPreDamageEvent;
    }

    public void OnHeroDamaged(Hero hero, Character attacker, int damageAmount)
    {
        HeroDamagedEvent heroDamagedEvent = new HeroDamagedEvent()
        {
            Hero = hero,
            Attacker = attacker,
            DamageAmount = damageAmount
        };

        HeroDamagedHandler.OnNext(heroDamagedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroDamaged.OnNext(heroDamagedEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroPreHeal.OnNext(heroPreHealEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroHealed.OnNext(heroHealedEvent);
        }
    }

    public void OnHeroGainedArmor(Hero hero, int armorAmount)
    {
        HeroGainedArmorEvent heroGainedArmorEvent = new HeroGainedArmorEvent()
        {
            Hero = hero,
            ArmorAmount = armorAmount
        };

        HeroGainedArmorHandler.OnNext(heroGainedArmorEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroGainedArmor.OnNext(heroGainedArmorEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnHeroEquippedWeapon.OnNext(heroEquippedWeaponEvent);
        }
    }

    public void OnInspired(Hero hero, BaseHeroPower heroPower)
    {
        InspireEvent inspireEvent = new InspireEvent()
        {
            Hero = hero,
            HeroPower = heroPower
        };

        InspireHandler.OnNext(inspireEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnInspired.OnNext(inspireEvent);
        }
    }

    public SpellPreCastEvent OnSpellPreCast(Player player, SpellCard spell, Character target)
    {
        SpellPreCastEvent spellPreCastEvent = new SpellPreCastEvent()
        {
            Player = player,
            Spell = spell,
            Target = target
        };

        SpellPreCastHandler.OnNext(spellPreCastEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnSpellPreCast.OnNext(spell);
        }

        return spellPreCastEvent;
    }

    public void OnSpellCasted(Player player, SpellCard spell)
    {
        SpellCastedEvent spellCastedEvent = new SpellCastedEvent()
        {
            Player = player,
            Spell = spell
        };

        SpellCastedHandler.OnNext(spellCastedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnSpellCasted.OnNext(spell);
        }
    }

    public void OnCardPlayed(Player player, BaseCard card)
    {
        CardPlayedEvent cardPlayedEvent = new CardPlayedEvent()
        {
            Player = player,
            Card = card
        };

        CardPlayedHandler.OnNext(cardPlayedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            // TODO : Trigger overloaded/battlecry card played events
            battlefieldMinion.Buffs.OnCardPlayed.OnNext(cardPlayedEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnCardDrawn.OnNext(cardDrawnEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnCardDiscarded.OnNext(cardDiscardedEvent);
        }
    }

    public void OnSecretPlayed(Player player, SpellCard secret)
    {
        SecretPlayedEvent secretPlayedEvent = new SecretPlayedEvent()
        {
            Player = player,
            Secret = secret
        };

        SecretPlayedHandler.OnNext(secretPlayedEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnSecretPlayed.OnNext(secretPlayedEvent);
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

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnSecretRevealed.OnNext(secretRevealedEvent);
        }
    }

    public void OnTurnStart(Player player)
    {
        TurnEvent turnStartEvent = new TurnEvent()
        {
            Player = player
        };

        TurnStartHandler.OnNext(turnStartEvent);

        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnTurnStart.OnNext(turnStartEvent);
        }
    }

    public void OnTurnEnd(Player player)
    {
        TurnEvent turnEndEvent = new TurnEvent()
        {
            Player = player
        };

        TurnStartHandler.OnNext(turnEndEvent);
        
        foreach (Minion battlefieldMinion in GameManager.Instance.GetAllMinions())
        {
            battlefieldMinion.Buffs.OnTurnEnd.OnNext(turnEndEvent);
        }
    }

    #endregion
}