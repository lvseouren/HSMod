using UnityEngine;
using System.Collections;

public class BasicMinionSuperClass : MonoBehaviour
{
    public int baseHealth;
    public int baseAttack;
    public int currentAttack;
    public int currentHealth;
    public int baseManaCost;
    public int currentManaCost;
    public string TAG;

    public bool hasSpellImmunity;
    public bool hasDivinieShield;
    public bool hasWindfury;
    public bool hasStealth;
    public bool hasCharge;
    public bool hasTaunt;
    public bool hasMegaWindfury;
    public bool hasMegaDivineShield;

    public bool hasTargetAbility;
    public bool canTargetAllies;
    public bool canTargetEnemies;
    public bool canTargetHeroes;
    public bool isFrozen;

    void OnEnable()
    {
        Events.minionDealDamageEvent += DealDamage;
    }

    void OnDisable()
    {
        Events.minionDealDamageEvent -= DealDamage;
    }

    public void DealDamage(BasicMinionSuperClass attacker, BasicMinionSuperClass defender)
    {
        // very simple for testing
        defender.currentHealth -= attacker.currentAttack;
    }

    public void Death()
    {
        Events.minionDeathrattleEvent += Deathrattle;
    }

    public void Deathrattle()
    {
        // do deathrattle stuff
        Events.minionDeathrattleEvent -= Deathrattle;
    }

    /* old guy stuff
	void OnEnable () {

	}

	void OnDisable () {
		// stop listening the methods

	}

	public virtual void OnSummon () {

	}

	public virtual void OnBattleCry () {
		BattleCry[] btcries =  (BattleCry[]) GetComponents<BattleCry> ();
		foreach ( BattleCry bc in btcries ){
			bc.OnBattleCry () ;
		}
	}

	public virtual void OnDeathRattle () {

	}
    */
}