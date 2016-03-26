using UnityEngine;
using System.Collections;

public class BasicSpellSuperClass : MonoBehaviour
{
    public int baseManaCost;
    public int currentManaCost;
    public int baseDamage;
    public int baseHealing;

    public bool hasSummonAbility;
    public bool hasHealingAbility;
    public bool canTarget;
    public bool canTargetEnemyHeroes;
    public bool canTargetSelf;
    public bool canTargetEnemyMinions;
    public bool canTargetFriendlyMinions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
