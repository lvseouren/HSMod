using System.Collections.Generic;
using System.Reactive.Subjects;

public class BuffManager
{
    public MinionCard Minion;
    public List<BaseBuff> AllBuffs = new List<BaseBuff>();

    #region Events

    public Subject<object> Battlecry = new Subject<object>();
    public Subject<object> Deathrattle = new Subject<object>();

    public Subject<object> OnPreAttack = new Subject<object>();
    public Subject<int> OnAttacked = new Subject<int>();

    public Subject<object> OnPreDamage = new Subject<object>();
    public Subject<object> OnDamaged = new Subject<object>();

    public Subject<object> OnInspired = new Subject<object>();

    // Played events may be fusioned in the future
    public Subject<BaseCard> OnCardDrawn = new Subject<BaseCard>();
    public Subject<object> OnCardDiscarded = new Subject<object>();
    public Subject<object> OnCardPlayed = new Subject<object>();
    public Subject<object> OnOverloadedCardPlayed = new Subject<object>();
    public Subject<object> OnBattlecryCardPlayed = new Subject<object>();

    public Subject<SpellCard> OnSpellPreCast = new Subject<SpellCard>();
    public Subject<SpellCard> OnSpellCasted = new Subject<SpellCard>();

    public Subject<object> OnSecretPlayed = new Subject<object>();
    public Subject<object> OnSecretRevealed = new Subject<object>();

    public Subject<MinionCard> OnMinionPlayed = new Subject<MinionCard>();
    public Subject<MinionCard> OnMinionSummoned = new Subject<MinionCard>();
    public Subject<MinionCard> OnMinionDamaged = new Subject<MinionCard>();
    public Subject<MinionCard> OnMinionHealed = new Subject<MinionCard>();
    public Subject<MinionCard> OnMinionDied = new Subject<MinionCard>();

    public Subject<object> OnCharacterHealed = new Subject<object>();

    public Subject<object> OnHeroDamaged = new Subject<object>();
    public Subject<object> OnHeroGainedArmor = new Subject<object>();
    public Subject<object> OnHeroEquippedWeapon = new Subject<object>(); // TODO in EventManager

    public Subject<SpellCard> OnTargetedBySpell = new Subject<SpellCard>();

    public Subject<object> OnTurnStart = new Subject<object>();
    public Subject<object> OnTurnEnd = new Subject<object>();

    #endregion

    public BuffManager(MinionCard minion)
    {
        Minion = minion;
    }

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
        OnMinionDamaged.Dispose();
        OnMinionHealed.Dispose();
        OnMinionDied.Dispose();

        OnCharacterHealed.Dispose();

        OnHeroDamaged.Dispose();
        OnHeroGainedArmor.Dispose();
        OnHeroEquippedWeapon.Dispose();
        
        OnTargetedBySpell.Dispose();

        OnTurnStart.Dispose();
        OnTurnEnd.Dispose();
    }
}