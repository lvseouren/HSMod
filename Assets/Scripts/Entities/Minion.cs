using System.Collections.Generic;
using UnityEngine;

public class Minion : Character
{
    public MinionCard Card;
    public BuffManager Buffs = new BuffManager();

    #region Constructor

    public Minion(MinionCard card)
    {
        Player = card.Player;
        Card = card;

        CurrentAttack = card.CurrentAttack;
        BaseAttack = card.BaseAttack;

        CurrentHealth = card.CurrentHealth;
        MaxHealth = card.CurrentAttack;
        BaseHealth = card.BaseHealth;

        HasTaunt = card.HasTaunt;
        HasCharge = card.HasCharge;
        HasPoison = card.HasPoison;
        HasWindfury = card.HasWindfury;
        HasDivineShield = card.HasDivineShield;
        IsElusive = card.IsElusive;
        IsForgetful = card.IsForgetful;
        IsStealth = card.IsStealth;

        SpellPower = card.SpellPower;

        CurrentArmor = 0;
    }

    #endregion

    #region Methods

    public override void Attack(Character target)
    {
        // Removing stealth of the Minion
        IsStealth = false;

        // Checking if minion is Forgetful
        if (IsForgetful)
        {
            // Checking if there's more than 1 enemy (hero + minions)
            if (Player.Enemy.Minions.Count > 0)
            {
                // Random 50% chance
                if (Random.Range(0, 2) == 1)
                {
                    // TODO : Play forgetful trigger animation

                    // Creating a list of possible targets
                    List<Character> possibleTargets = new List<Character>();

                    // Adding the enemy hero to the list
                    possibleTargets.Add(Player.Enemy.Hero);

                    // Adding all enemy minions to the list
                    foreach (Minion enemyMinion in Player.Enemy.Minions)
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
        Buffs.OnPreAttack.OnNext(null);
        MinionPreAttackEvent minionPreAttackEvent = EventManager.Instance.OnMinionPreAttack(this, target);

        // Checking if the Attack was not cancelled
        if (minionPreAttackEvent.Status != PreStatus.Cancelled)
        {
            // Redefining target in case it changed when firing events
            target = minionPreAttackEvent.Target;

            // Getting the minion attack
            int minionAttack = CurrentAttack;
            int targetAttack = target.CurrentAttack;
            
            if (target.IsHero())
            {
                target.TryDamage(this, minionAttack);
            }
            else if (target.IsMinion())
            {
                // Damaging both minions
                this.TryDamage(target, targetAttack);
                target.TryDamage(this, minionAttack);

                // Checking the death of both characters
                this.CheckDeath();
                target.CheckDeath();
            }

            // Firing OnAttacked events
            Buffs.OnAttacked.OnNext(null);
            EventManager.Instance.OnMinionAttacked(this, target);
        }
    }

    public override void TryDamage(Character attacker, int damageAmount)
    {
        // TODO : Gotta make the BuffManager methods to be able to modify the damage amounts

        // Firing OnPreDamage events
        MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(this, attacker, damageAmount);
        Buffs.OnPreDamage.OnNext(minionPreDamageEvent.DamageAmount);

        if (attacker == null)
        {
            Damage(minionPreDamageEvent.DamageAmount);

            EventManager.Instance.OnMinionDamaged(this, attacker, damageAmount);
        }
        else if (attacker.IsAlive())
        {
            Damage(minionPreDamageEvent.DamageAmount);

            EventManager.Instance.OnMinionDamaged(this, attacker, damageAmount);

            if (attacker.HasPoison)
            {
                if (minionPreDamageEvent.DamageAmount > 0)
                {
                    EventManager.Instance.OnMinionPoisoned(this, attacker);

                    Destroy();
                }
            }
        }

        CheckDeath();
    }

    public override void Heal(int healAmount)
    {
        // Firing OnMinionPreHeal events
        MinionPreHealEvent minionPreHealEvent = EventManager.Instance.OnMinionPreHeal(this, healAmount);

        int healeableHealth = MaxHealth - CurrentHealth;

        if (minionPreHealEvent.HealAmount > healeableHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += minionPreHealEvent.HealAmount;
        }

        // Firing OnMinionHealed events
        EventManager.Instance.OnMinionHealed(this, minionPreHealEvent.HealAmount);

        // TODO : Heal animation
        // TODO : Show heal sprite + healed amount

        Controller.UpdateNumbers();
    }

    private void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        // TODO : Show health loss sprite + amount on token

        Controller.UpdateNumbers();
    }

    public override void CheckDeath()
    {
        if (IsAlive() == false)
        {
            // Firing Deathrattles and OnDied events
            Buffs.Deathrattle.OnNext(this);
            EventManager.Instance.OnMinionDied(this);

            // Removing the minion from the Player board
            Player.BoardController.RemoveMinion(this);

            // Destroying the controller
            Controller.DestroyController();

            // Adding the card to the list of dead Minions
            Player.DeadMinions.Add(Card);
        }
    }

    public void AddBuff(BaseBuff buff)
    {
        // Adding the buff to the list
        Buffs.AllBuffs.Add(buff);

        // Firing OnAdded for that buff
        buff.OnAdded(this);
    }

    public void RemoveBuff(BaseBuff buff)
    {
        // Checking if the minion has the buff
        if (Buffs.AllBuffs.Contains(buff))
        {
            // Removing the buff from the list
            Buffs.AllBuffs.Remove(buff);

            // Firing OnRemoved for that buff
            buff.OnRemoved(this);
        }
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
        // TODO : Play animations + sound and add silenced sprite

        Buffs.RemoveAll();

        HasTaunt = false;
        HasCharge = false;
        HasPoison = false;
        HasWindfury = false;
        HasDivineShield = false;
        IsImmune = false;
        IsElusive = false;
        IsStealth = false;

        SpellPower = 0;

        IsFrozen = false;
        UnfreezeNextTurn = false;
        IsForgetful = false;

        IsSilenced = true;
    }

    public void Transform(Minion other)
    {
        // TODO : Play transform animation
        // TODO : Transform minion without triggering anything, destroy old minion
    }

    public void Die()
    {
        // Firing OnMinionDied events
        Buffs.Deathrattle.OnNext(this);
        EventManager.Instance.OnMinionDied(this);

        Buffs.RemoveAll();

        // TODO : Custom animations, sounds, etc ?
        // TODO : Add minion to list of dead minions

        Destroy();
    }

    public void Destroy()
    {
        // TODO : Play destroy animation (dust and stuff)
        // TODO : Remove card from battlefield
        // TODO : Place on graveyard? (interaction with cards like ressurect etc)
    }

    #endregion

    #region Getter Methods

    #endregion

    #region Condition Checkers

    public override bool IsMinion()
    {
        return true;
    }

    public override bool CanAttack()
    {
        if (IsFrozen == false)
        {
            if (IsSleeping == false || HasCharge)
            {
                if (HasWindfury)
                {
                    return CurrentTurnAttacks < 2;
                }
                else
                {
                    return CurrentTurnAttacks < 1;
                }
            }
        }
        return false;
    }

    #endregion
}