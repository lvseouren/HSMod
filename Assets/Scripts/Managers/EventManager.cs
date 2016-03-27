using System.Reactive.Subjects;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Minion Events //
    public Subject<MinionPlayedEvent> MinionPlayedHandler = new Subject<MinionPlayedEvent>();
    public Subject<MinionPreAttackEvent> MinionPreAttackHandler = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> MinionAttackedHandler = new Subject<MinionAttackedEvent>();
    public Subject<MinionDamagedEvent> MinionDamagedHandler = new Subject<MinionDamagedEvent>();
    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    // Hero Events //
    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroAttackedEvent> HeroAttackedHandler = new Subject<HeroAttackedEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();

    // Spell Events //
    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastEvent> SpellCastHandler = new Subject<SpellCastEvent>();

    // Card Events //
    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();
    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

    public void Start()
    {
        // TODO : SETUP
    }

    public void OnMinionPlayed(Player player, MinionCard minion)
    {
        MinionPlayedEvent minionPlayedEvent = new MinionPlayedEvent()
        {
            Player = player,
            Minion = minion
        };

        MinionPlayedHandler.OnNext(minionPlayedEvent);
    }

    public void OnMinionPreAttack(MinionCard minion, IDamageable target)
    {
        MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionPreAttackHandler.OnNext(minionPreAttackEvent);

        if (minionPreAttackEvent.IsCancelled == false)
        {
            minion.Attack(target);
        }
    }

    public void OnMinionAttacked(MinionCard minion, IDamageable target)
    {
        MinionPreAttackEvent minionPreAttackEvent = new MinionPreAttackEvent()
        {
            Minion = minion,
            Target = target
        };

        MinionPreAttackHandler.OnNext(minionPreAttackEvent);
    }

    public void OnMinionDamaged(IDamageable attacker, MinionCard minion, int damage)
    {
        MinionDamagedEvent minionDamagedEvent = new MinionDamagedEvent()
        {
            Attacker = attacker,
            Minion = minion,
            Damage = damage
        };

        MinionDamagedHandler.OnNext(minionDamagedEvent);
    }

    // TODO : Make a new interface IDamager ? spells can kill but they can't be killed...
    public void OnMinionDied(IDamageable killer, MinionCard minion)
    {
        MinionDiedEvent minionDiedEvent = new MinionDiedEvent()
        {
            Killer = killer,
            Minion = minion
        };

        MinionDiedHandler.OnNext(minionDiedEvent);
    }
}