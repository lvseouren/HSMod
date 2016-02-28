using UnityEngine;
using System.Collections;

public class BasicMinionSuperClass : MonoBehaviour {


	public int baseHealth ;
	public int baseAttack ;
	public int currentAttack ;
	public int currentHealth ;
	public int manaCost ;
	public string TAG ;

	public bool hasDivinieShield ;
	public bool hasWindfury ;
	public bool hasStealth ;
	public bool hasCharge ;
    public bool hasMegaWindfury ;
    public bool hasMegaDivineShield;

	public bool hasTargetAbility ;
	public bool canTargetAllies ;
	public bool canTargetEnemies ;
	public bool canTargetHeroes ;
	public bool isFrozen ;

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

}