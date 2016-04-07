using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MinionCard : BaseCard, ICharacter
{
    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }
    public MinionType MinionType;

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    // Effects //
    public bool Taunt = false;
    public bool Charge = false;
    public bool Stealth = false;
    public bool DivineShield = false;
    public bool Elusive = false;
    public bool Forgetful = false;
    public bool Frozen = false;
    public bool Silenced = false;
    public int SpellPower = 0;

    public BuffManager BuffManager;

    public void Start()
    {
         BuffManager = new BuffManager(this);
    }

    #region Methods

    public void AddBuff(BaseBuff buff)
    {
        // Adding the buff to the list
        BuffManager.AllBuffs.Add(buff);

        // Firing OnAdded for that buff
        buff.OnAdded(this);
    }

    public void RemoveBuff(BaseBuff buff)
    {
        if (BuffManager.AllBuffs.Contains(buff))
        {
            // Removing the buff from the list
            BuffManager.AllBuffs.Remove(buff);

            // Firing OnRemoved for that buff
            buff.OnRemoved(this);
        }
    }

    public void Attack(ICharacter target)
    {
        // Checking if minion is forgetful
        if (this.Forgetful)
        {
            // Checking if there's more than 1 enemy (hero + minions)
            if (this.Player.Enemy.Minions.Count > 0)
            {
                // Random 50% chance
                if (Random.Range(0, 1) == 1)
                {
                    // TODO : Play forgetful trigger animation

                    // Creating a list of possible targets
                    List<ICharacter> possibleTargets = new List<ICharacter>();

                    // Adding the enemy hero to the list
                    possibleTargets.Add(this.Player.Enemy.Hero);

                    // Adding all enemy minions to the list
                    foreach (MinionCard enemyMinion in this.Player.Enemy.Minions)
                    {
                        possibleTargets.Add(enemyMinion);
                    }

                    // Removing the current target from the possible targets list
                    possibleTargets.Remove(target);

                    // Selecting a target by random
                    int randomTarget = Random.Range(0, possibleTargets.Count);

                    // Setting the current target as the random target
                    target = possibleTargets[randomTarget];
                }
            }
        }

        // Firing OnPreAttack events
        this.BuffManager.OnPreAttack.OnNext(null);
        MinionPreAttackEvent minionPreAttackEvent = EventManager.Instance.OnMinionPreAttack(this, target);

        // Checking if the Attack was cancelled
        if (minionPreAttackEvent.IsCancelled == false)
        {
            // Getting the attacker minion attack value
            int attackerAttack = this.CurrentAttack;

            // Checking the target type
            if (target is Hero)
            {
                // Casting ICharacter to Hero
                Hero heroTarget = (Hero) target;

                // Firing OnHeroPreDamage event
                HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, heroTarget);

                // Checking if the Attack was cancelled
                if (heroPreDamageEvent.IsCancelled == false)
                {
                    // Attacking the target hero
                    heroTarget.Damage(this.CurrentAttack);

                    // Firing OnHeroDamaged event
                    EventManager.Instance.OnHeroDamaged(this, heroTarget, attackerAttack);
                }
            }
            else if (target is MinionCard)
            {
                // Casting ICharacter to MinionCard
                MinionCard minionTarget = (MinionCard) target;

                // Firing OnMinionPreDamage event
                MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(this, minionTarget);

                // Checking if the attack was cancelled
                if (minionPreDamageEvent.IsCancelled == false)
                {
                    // Getting the attack value of the enemy minion
                    int targetAttack = minionTarget.CurrentAttack;

                    // Damaging both minions
                    minionTarget.Damage(attackerAttack);
                    this.Damage(targetAttack);

                    // Triggering specific and global events for both minions
                    minionTarget.BuffManager.OnDamaged.OnNext(null);
                    EventManager.Instance.OnMinionDamaged(this, minionTarget);

                    this.BuffManager.OnDamaged.OnNext(null);
                    EventManager.Instance.OnMinionDamaged(minionTarget, this);

                    // Checking death of both minions
                    minionTarget.CheckDie();
                    this.CheckDie();
                }
            }

            // Firing OnAttacked events
            this.BuffManager.OnAttacked.OnNext(attackerAttack);
            EventManager.Instance.OnMinionAttacked(this, target);
        }
    }

    public void Damage(int damageAmount)
    {
        this.BaseHealth -= damageAmount;
        // TODO : Sprite -> Show health loss on card
    }

    public void Spawn()
    {
        // TODO : Custom animations, sounds, etc ?
        // TODO : Position in battlefield
        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
    }

    public void CheckDie()
    {
        if (IsAlive() == false)
        {
            Die();
        }
    }

    public void Die()
    {
        //EventManager.OnMinionDied(this);
        // TODO : Custom animations, sounds, etc ?
        // TODO : Add card to list of dead minions
        Destroy();
    }

    public void ReturnToHand()
    {
        // TODO
    }

    public void ReturnToDeck()
    {
        // TODO
    }

    public void Silence()
    {
        Taunt = false;
        Charge = false;
        Stealth = false;
        DivineShield = false;
        Elusive = false;
        Forgetful = false;
        Frozen = false;
        Silenced = false;
        SpellPower = 0;

        BuffManager.RemoveAll();
    }

    public void Destroy()
    {
        // TODO : Play destroy animation (dust and stuff)
        // TODO : Remove card from battlefield
    }

    public void Transform(MinionCard other)
    {
        // TODO : Play transform animation
        // TODO : Transform minion without triggering anything
    }

    #endregion

    #region Condition Checkers

    public bool IsAlive()
    {
        return this.CurrentHealth > 0;
    }

    public bool IsEnemyOf(MinionCard otherMinion)
    {
        return this.Player != otherMinion.Player;
    }

    public bool IsFriendlyOf(MinionCard otherMinion)
    {
        return this.Player == otherMinion.Player;
    }

    #endregion
}

public enum MinionType
{
    // Classic Types //
    General,
    Murloc,
    Beast,
    Mech,
    Dragon,
    Pirate,
    Demon,
    Totem,

    // Custom Types //
    Undead
}