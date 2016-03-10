using UnityEngine;
using System.Collections;

// For example usage and explanation see: AnotherEventManager.cs
// TODO: custom event arguments class/container to reduce amount of potential events.
public class Events : MonoBehaviour {
    // more events will be added on the go.
    public delegate void OnMinionAttack(BasicMinionSuperClass attacker, BasicMinionSuperClass defender);
    public delegate void OnMinionDeath();
    public delegate void OnMinionTakeDamage(BasicMinionSuperClass minion, int damage);
    public delegate void OnMinionDealDamage(BasicMinionSuperClass attacker, BasicMinionSuperClass defender);
    public delegate void OnMinionDeathrattle();

    public static event OnMinionAttack minionAttackEvent;
    public static event OnMinionDeath minionDeathEvent;
    public static event OnMinionDealDamage minionDealDamageEvent;
    public static event OnMinionTakeDamage minionTakeDamageEvent;
    public static event OnMinionDeathrattle minionDeathrattleEvent;

    // Methods used to call events:

    public static void OnMinionDeathEvent()
    {
        minionDeathEvent();
    }

    public static void OnMinionDeathrattleEvent()
    {
        minionDeathrattleEvent();
    }


}
