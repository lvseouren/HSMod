using System.Reactive.Subjects;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Subject<MinionPlayedEvent> MinionPlayedHandler = new Subject<MinionPlayedEvent>();
    public Subject<MinionPreAttackEvent> MinionPreAttackHandler = new Subject<MinionPreAttackEvent>();
    public Subject<MinionAttackedEvent> MinionAttackedHandler = new Subject<MinionAttackedEvent>();
    public Subject<MinionDamagedEvent> MinionDamagedHandler = new Subject<MinionDamagedEvent>();
    public Subject<MinionDiedEvent> MinionDiedHandler = new Subject<MinionDiedEvent>();

    public Subject<HeroPreAttackEvent> HeroPreAttackHandler = new Subject<HeroPreAttackEvent>();
    public Subject<HeroDamagedEvent> HeroDamagedHandler = new Subject<HeroDamagedEvent>();

    public Subject<SpellPreCastEvent> SpellPreCastHandler = new Subject<SpellPreCastEvent>();
    public Subject<SpellCastEvent> SpellCastHandler = new Subject<SpellCastEvent>();

    public Subject<CardDrawnEvent> CardDrawnHandler = new Subject<CardDrawnEvent>();
    public Subject<CardDiscardedEvent> CardDiscardedHandler = new Subject<CardDiscardedEvent>();

    public void Start()
    {
        // TODO : SETUP
    }
}