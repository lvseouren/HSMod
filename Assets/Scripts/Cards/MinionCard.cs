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
    public int TurnAttacks = 0;
    public bool Sleeping = true;

    // Effects //
    public bool Taunt = false;
    public bool Charge = false;
    public bool Stealth = false;
    public bool DivineShield = false;
    public bool Elusive = false;
    public bool Forgetful = false;
    public bool Frozen = false;
    public bool Silenced = false;
    public bool Windfury = false;
    public int SpellPower = 0;

    public BuffManager BuffManager;

    public void Start()
    {
         BuffManager = new BuffManager(this);
    }

    public void AddBuff(BaseBuff buff)
    {
        // Adding the buff to the list
        BuffManager.AllBuffs.Add(buff);

        // Firing OnAdded for that buff
        buff.OnAdded(this);
    }

    public void RemoveBuff(BaseBuff buff)
    {
        // Checking if the minion has the buff
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
                if (Random.Range(0, 2) == 1)
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

        // Checking if the Attack was not cancelled
        if (minionPreAttackEvent.Status != PreStatus.Cancelled)
        {
            // Redefining target in case it changed when firing events
            target = minionPreAttackEvent.Target;

            // Target is a Hero
            if (target.IsHero())
            {
                target.As<Hero>().TryDamage(this, this.CurrentAttack);
            }

            // Target is a Minion
            else if (target.IsMinion())
            {
                // Casting ICharacter to MinionCard
                MinionCard targetMinion = target.As<MinionCard>();

                // Getting both minions attack
                int attackerAttack = this.CurrentAttack;
                int targetAttack = target.CurrentAttack;

                // Damaging both minions
                this.TryDamage(targetMinion, targetAttack);
                targetMinion.TryDamage(this, attackerAttack);

                // Checking the death of both characters
                this.CheckDeath();
                targetMinion.CheckDeath();
            }

            // Firing OnAttacked events
            this.BuffManager.OnAttacked.OnNext(null);
            EventManager.Instance.OnMinionAttacked(this, target);
        }
    }

    public void TryDamage(ICharacter attacker, int damageAmount)
    {
        MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(this, attacker, damageAmount);

        if (attacker.IsAlive())
        {
            this.Damage(minionPreDamageEvent.Damage);

            EventManager.Instance.OnMinionDamaged(this, attacker, damageAmount);
        }
    }

    public void Damage(int damageAmount)
    {
        this.BuffManager.OnPreDamage.OnNext(damageAmount);

        this.BaseHealth -= damageAmount;

        // TODO : Sprite -> Show health loss on token
    }

    public void Heal(int healAmount)
    {
        // Firing OnMinionPreHeal events
        MinionPreHealEvent minionPreHealEvent = EventManager.Instance.OnMinionPreHeal(this, healAmount);

        int healeableHealth = MaxHealth - CurrentHealth;

        if (minionPreHealEvent.HealAmount > healeableHealth)
        {
            this.CurrentHealth = MaxHealth;
        }
        else
        {
            this.CurrentHealth += minionPreHealEvent.HealAmount;
        }

        // Firing OnMinionHealed events
        EventManager.Instance.OnMinionHealed(this, minionPreHealEvent.HealAmount);

        // TODO : Heal animation
        // TODO : Show heal sprite + healed amount
    }

    public void Spawn()
    {
        // TODO : Call custom animations, sounds, etc ?
        // TODO : Positioning in battlefield

        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
    }

    public void CheckDeath()
    {
        if (this.IsAlive() == false)
        {
            Die();
        }
    }

    public void Die()
    {
        // Firing OnMinionDied events
        this.BuffManager.Deathrattle.OnNext(this);
        EventManager.Instance.OnMinionDied(this);

        this.BuffManager.RemoveAll();

        // TODO : Custom animations, sounds, etc ?
        // TODO : Add minion to list of dead minions

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
        Windfury = false;
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
        // TODO : Transform minion without triggering anything, destroy old minion
    }

    public virtual bool CanAttack()
    {
        return true;
    }

    public virtual bool CanTarget(ICharacter target)
    {
        return true;
    }
}