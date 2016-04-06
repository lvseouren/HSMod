﻿using System;
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
    public int SpellDamage = 0;

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
        buff.OnAdded();
    }

    public void RemoveBuff(BaseBuff buff)
    {
        if (BuffManager.AllBuffs.Contains(buff))
        {
            // Removing the buff from the list
            BuffManager.AllBuffs.Remove(buff);

            // Firing OnAdded for that buff
            buff.OnRemoved();
        }
    }

    public void Attack(ICharacter target)
    {
        // TODO : Check for enemy count > 0
        if (this.Forgetful)
        {
            if (Random.Range(0, 1) == 1)
            {
                // target = select a random target
            }
        }

        // Firing OnPreAttack events
        this.BuffManager.OnPreAttack.OnNext(null);
        MinionPreAttackEvent minionPreAttackEvent = EventManager.Instance.OnMinionPreAttack(this, target);

        // Checking if the Attack was cancelled
        if (minionPreAttackEvent.IsCancelled == false)
        {
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
                    // Getting the atttack values
                    int minionAttack = this.CurrentAttack;

                    // Attacking the target hero
                    heroTarget.Damage(this.CurrentAttack);

                    // Firing OnHeroDamaged event
                    EventManager.Instance.OnHeroDamaged(this, heroTarget, minionAttack);
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
                    // Getting the atttack values
                    int attackerAttack = this.CurrentAttack;
                    int targetAttack = minionTarget.CurrentAttack;

                    // Damaging both minions
                    minionTarget.Damage(attackerAttack);
                    this.Damage(targetAttack);

                    // Triggering specific and global events for both minions
                    minionTarget.OnDamaged(this, attackerAttack);
                    EventManager.Instance.OnMinionDamaged(this, minionTarget);

                    this.OnDamaged(minionTarget, targetAttack);
                    EventManager.Instance.OnMinionDamaged(minionTarget, this);

                    // Checking death of both minions
                    minionTarget.CheckDie();
                    this.CheckDie();
                }
            }

            // Firing OnAttacked events
            this.OnAttacked();
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
        SpellDamage = 0;

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