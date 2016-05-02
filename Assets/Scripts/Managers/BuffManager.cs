using System.Collections.Generic;
using System.Reactive.Subjects;

public class BuffManager
{
    public MinionCard Minion;
    public List<BaseBuff> AllBuffs = new List<BaseBuff>();

    #region Events

    // Self Events //
    public Subject<object> Battlecry = new Subject<object>();
    public Subject<MinionCard> Deathrattle = new Subject<MinionCard>();

    public Subject<object> OnPreAttack = new Subject<object>();
    public Subject<object> OnAttacked = new Subject<object>();

    public Subject<int> OnPreDamage = new Subject<int>();
    public Subject<MinionDamagedEvent> OnDamaged = new Subject<MinionDamagedEvent>();

    public Subject<HeroPowerEvent> OnInspired = new Subject<HeroPowerEvent>();

    // Global Events //
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
    public Subject<MinionCard> OnMinionSummoned = new Subject<MinionCard>();
    public Subject<MinionPreAttackEvent> OnMinionPreAttack = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> OnMinionAttacked = new Subject<MinionAttackedEvent>();
    public Subject<MinionPreDamageEvent> OnMinionPreDamage = new Subject<MinionPreDamageEvent>();
    public Subject<MinionDamagedEvent> OnMinionDamaged = new Subject<MinionDamagedEvent>();
    public Subject<MinionPreHealEvent> OnMinionPreHeal = new Subject<MinionPreHealEvent>();
    public Subject<MinionHealedEvent> OnMinionHealed = new Subject<MinionHealedEvent>();
    public Subject<MinionDiedEvent> OnMinionDied = new Subject<MinionDiedEvent>();
    public Subject<MinionPoisonedEvent> OnMinionPoisoned = new Subject<MinionPoisonedEvent>();

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

    public Subject<object> OnTurnStart = new Subject<object>();
    public Subject<object> OnTurnEnd = new Subject<object>();

    #endregion
    
    public void RemoveAll()
    {
        // Looping on all buffs
        foreach (BaseBuff buff in AllBuffs)
        {
            // Checking if buff is not an area buff
            if (buff.BuffType != BuffType.Area)
            {
                // Removing stats/effects from the buff
                buff.OnRemoved(this.Minion);
            }
        }

        // Removing the buffs from the list
        AllBuffs.RemoveAll(buff => buff.BuffType != BuffType.Area);

        // Disposing all subscribed events
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