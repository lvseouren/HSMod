using System.Collections.Generic;
using System.Reactive.Subjects;

public class BuffManager
{
    public Minion Minion;

    public List<BaseBuff> AllBuffs = new List<BaseBuff>();

    #region Self Events

    public Subject<object> Battlecry = new Subject<object>();
    public Subject<Minion> Deathrattle = new Subject<Minion>();

    public Subject<object> OnPreAttack = new Subject<object>();
    public Subject<object> OnAttacked = new Subject<object>();

    public Subject<MinionPreDamageEvent> OnPreDamage = new Subject<MinionPreDamageEvent>();
    public Subject<MinionDamagedEvent> OnDamaged = new Subject<MinionDamagedEvent>();

    public Subject<InspireEvent> OnInspired = new Subject<InspireEvent>();

    #endregion

    #region Global Events

    public Subject<CardDrawnEvent> OnCardDrawn = new Subject<CardDrawnEvent>();
    public Subject<CardDiscardedEvent> OnCardDiscarded = new Subject<CardDiscardedEvent>();
    public Subject<CardPlayedEvent> OnCardPlayed = new Subject<CardPlayedEvent>();
    public Subject<object> OnOverloadedCardPlayed = new Subject<object>();
    public Subject<object> OnBattlecryCardPlayed = new Subject<object>();

    public Subject<SpellCard> OnSpellPreCast = new Subject<SpellCard>();
    public Subject<SpellCard> OnSpellCasted = new Subject<SpellCard>();

    public Subject<SecretPlayedEvent> OnSecretPlayed = new Subject<SecretPlayedEvent>();
    public Subject<SecretRevealedEvent> OnSecretRevealed = new Subject<SecretRevealedEvent>();

    public Subject<MinionPlayedEvent> OnMinionPlayed = new Subject<MinionPlayedEvent>();
    public Subject<Minion> OnMinionSummoned = new Subject<Minion>();
    public Subject<MinionPreAttackEvent> OnMinionPreAttack = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> OnMinionAttacked = new Subject<MinionAttackedEvent>();
    public Subject<MinionPreDamageEvent> OnMinionPreDamage = new Subject<MinionPreDamageEvent>();
    public Subject<MinionDamagedEvent> OnMinionDamaged = new Subject<MinionDamagedEvent>();
    public Subject<MinionPreHealEvent> OnMinionPreHeal = new Subject<MinionPreHealEvent>();
    public Subject<MinionHealedEvent> OnMinionHealed = new Subject<MinionHealedEvent>();
    public Subject<MinionDiedEvent> OnMinionDied = new Subject<MinionDiedEvent>();
    public Subject<MinionPoisonedEvent> OnMinionPoisoned = new Subject<MinionPoisonedEvent>();
    public Subject<MinionFrozenEvent> OnMinionFrozen = new Subject<MinionFrozenEvent>();
	public Subject<MinionEnragedEvent> OnMinionEnraged = new Subject<MinionEnragedEvent>();

    public Subject<object> OnCharacterPreHeal = new Subject<object>();
    public Subject<object> OnCharacterHealed = new Subject<object>();
    
    public Subject<HeroPreAttackEvent> OnHeroPreAttack = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackedEvent> OnHeroAttacked = new Subject<HeroAttackedEvent>();
    public Subject<HeroPreDamageEvent> OnHeroPreDamage = new Subject<HeroPreDamageEvent>();
    public Subject<HeroDamagedEvent> OnHeroDamaged = new Subject<HeroDamagedEvent>();
    public Subject<HeroPreHealEvent> OnHeroPreHeal = new Subject<HeroPreHealEvent>();
    public Subject<HeroHealedEvent> OnHeroHealed = new Subject<HeroHealedEvent>();
    public Subject<HeroGainedArmorEvent> OnHeroGainedArmor = new Subject<HeroGainedArmorEvent>();
    public Subject<HeroEquippedWeaponEvent> OnHeroEquippedWeapon = new Subject<HeroEquippedWeaponEvent>();

    public Subject<SpellCard> OnTargetedBySpell = new Subject<SpellCard>();

    public Subject<TurnEvent> OnTurnStart = new Subject<TurnEvent>();
    public Subject<TurnEvent> OnTurnEnd = new Subject<TurnEvent>();

    #endregion
    
    public void RemoveAll()
    {
        // Iterating on the list of buffs
        foreach (BaseBuff buff in AllBuffs)
        {
            // Checking if the buff is not an area buff
            if (buff.BuffType != BuffType.Area)
            {
                // Removing stats/effects from the buff
                buff.OnRemoved(Minion);
            }
        }

        // Removing the buffs from the list
        AllBuffs.RemoveAll(buff => buff.BuffType != BuffType.Area);

        // Disposing all subscribed handlers
        Battlecry.Dispose();
        Deathrattle.Dispose();

        OnPreAttack.Dispose();
        OnAttacked.Dispose();

        OnPreDamage.Dispose();
        OnDamaged.Dispose();

        OnInspired.Dispose();

        OnCardDrawn.Dispose();
        OnCardDiscarded.Dispose();
        OnCardPlayed.Dispose();
        OnOverloadedCardPlayed.Dispose();
        OnBattlecryCardPlayed.Dispose();

        OnSpellPreCast.Dispose();
        OnSpellCasted.Dispose();
        
        OnSecretPlayed.Dispose();
        OnSecretRevealed.Dispose();
        
        OnMinionPlayed.Dispose();
        OnMinionSummoned.Dispose();
        OnMinionPreAttack.Dispose();
        OnMinionAttacked.Dispose();
        OnMinionPreDamage.Dispose();
        OnMinionDamaged.Dispose();
        OnMinionPreHeal.Dispose();
        OnMinionHealed.Dispose();
        OnMinionDied.Dispose();
        OnMinionPoisoned.Dispose();
        OnMinionFrozen.Dispose();
		OnMinionEnraged.Dispose();

        OnCharacterPreHeal.Dispose();
        OnCharacterHealed.Dispose();

        OnHeroPreAttack.Dispose();
        OnHeroAttacked.Dispose();
        OnHeroPreDamage.Dispose();
        OnHeroDamaged.Dispose();
        OnHeroPreHeal.Dispose();
        OnHeroHealed.Dispose();
        OnHeroGainedArmor.Dispose();
        OnHeroEquippedWeapon.Dispose();
        
        OnTargetedBySpell.Dispose();

        OnTurnStart.Dispose();
        OnTurnEnd.Dispose();
    }
}