using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public HeroClass Class;

    #region Methods

    public override void Attack(Character target)
    {
        // Checking if Hero is forgetful
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

                    // Adding all the enemies to the list
                    foreach (Minion enemyMinion in Player.Enemy.Minions)
                    {
                        possibleTargets.Add(enemyMinion);
                    }
                    possibleTargets.Add(Player.Enemy.Hero);

                    // Removing the current target from the possible targets list
                    possibleTargets.Remove(target);

                    // Setting the current target as the random target
                    target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                }
            }
        }

        // Firing OnPreAttack events
        HeroPreAttackEvent heroPreAttackEvent = EventManager.Instance.OnHeroPreAttack(this, target);

        // Checking if the Attack was cancelled
        if (heroPreAttackEvent.Status != PreStatus.Cancelled)
        {
            // Adding 1 to the current turn attacks counter
            CurrentTurnAttacks++;

            // Redefining target in case it changed when firing events
            target = heroPreAttackEvent.Target;

            int heroAttack = GetHeroAttack();
            
            if (target.IsHero())
            {
                target.TryDamage(this, heroAttack);
            }
            else
            {
                // Getting the minion attack
                int minionAttack = target.CurrentAttack;
                
                // Damaging both characters
                this.TryDamage(target, minionAttack);
                target.TryDamage(this, heroAttack);

                // Checking the death of both characters
                this.CheckDeath();
                target.CheckDeath();
            }

            // Firing OnAttacked events
            EventManager.Instance.OnHeroAttacked(this, target);
        }
    }

    public override void TryDamage(Character attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, attacker, damageAmount);

        if (attacker.IsAlive())
        {
            this.Damage(heroPreDamageEvent.DamageAmount);

            EventManager.Instance.OnHeroDamaged(this, attacker, heroPreDamageEvent.DamageAmount);
        }
    }

    public override void Heal(int healAmount)
    {
        // Firing OnHeroPreHeal events 
        HeroPreHealEvent heroPreHealEvent = EventManager.Instance.OnHeroPreHeal(this, healAmount);

        // TODO : Check if heal is transformed to damage and if so, call trydamage

        // Updating the current health (clamped to MaxHealth)
        CurrentHealth = Mathf.Min(CurrentHealth + heroPreHealEvent.HealAmount, MaxHealth);

        // Firing OnHeroHealed events 
        EventManager.Instance.OnHeroHealed(this, heroPreHealEvent.HealAmount);

        // TODO : Heal animation + sound
        // TODO : Show heal sprite + healed amount on hero portrait

        Player.HeroController.UpdateNumbers();
    }

    public override void CheckDeath()
    {
        if (this.IsAlive() == false)
        {
            // TODO : End game
        }
    }

    private void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        // TODO : Show health loss sprite + amount on hero portrait

        Player.HeroController.UpdateNumbers();
    }

    #endregion

    #region Getter Methods

    public int GetHeroAttack()
    {
        int attack = CurrentAttack;

        if (Player.HasWeapon())
        {
            attack += Player.Weapon.CurrentAttack;
        }

        return attack;
    }

    #endregion

    #region Condition Checkers

    public override bool CanAttack()
    {
        return true; // TODO : Delete this (true for testing purposes)

        if (IsFrozen == false)
        {
            if (CurrentAttack > 0 || Player.HasWeapon())
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