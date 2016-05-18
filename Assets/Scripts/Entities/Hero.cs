using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public BaseHeroPower HeroPower;

    public HeroClass Class;

    #region Methods

    public override void Attack(Character target)
    {
        Debugger.LogHero(this, "starting attack to " + target.GetName());

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

                    Debugger.LogHero(this, "switched target to " + target.TypeName() + " (forgetful)");
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

            // Getting both characters current attack
            int heroAttack = GetHeroAttack();
            int targetAttack = target.CurrentAttack;

            Debugger.LogHero(this, "attacking " + target.GetName());

            if (target.IsHero())
            {
                target.Damage(this, heroAttack);
            }
            else
            {
                // Damaging both characters
                this.Damage(target, targetAttack);
                target.Damage(this, heroAttack);

                // Checking the death of both characters
                this.CheckDeath();
                target.CheckDeath();
            }

            // Firing OnAttacked events
            EventManager.Instance.OnHeroAttacked(this, target);
        }
    }

    public override void Damage(Character attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, attacker, damageAmount);

        if (attacker != null)
        {
            if (attacker.IsAlive())
            {
                Debugger.LogHero(this, "receiving " + heroPreDamageEvent.DamageAmount + " damage by " + attacker.GetName());

                Damage(heroPreDamageEvent.DamageAmount);

                EventManager.Instance.OnHeroDamaged(this, attacker, heroPreDamageEvent.DamageAmount);
            }
        }
        else
        {
            Debugger.LogHero(this, "receiving " + heroPreDamageEvent.DamageAmount + " damage by " + attacker.GetName());

            Damage(heroPreDamageEvent.DamageAmount);

            EventManager.Instance.OnHeroDamaged(this, null, heroPreDamageEvent.DamageAmount);
        }
    }

    public override void Heal(int healAmount)
    {
        // Firing OnHeroPreHeal events 
        HeroPreHealEvent heroPreHealEvent = EventManager.Instance.OnHeroPreHeal(this, healAmount);

        // TODO : Check if heal is transformed to damage and if so, call Damage instead

        Debugger.LogHero(this, "healing for " + heroPreHealEvent.HealAmount);

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
        if (IsAlive() == false)
        {
            // TODO : End game
        }
    }

    private void Damage(int damageAmount)
    {
        Debugger.LogHero(this, "receiving " + damageAmount + " damage");

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

    public override bool IsHero()
    {
        return true;
    }

    public override bool CanAttack()
    {
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