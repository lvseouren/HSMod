using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, ICharacter
{
    // Parent //
    public Player Player;

    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }
    public HeroClass Class;

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Armor = 0;
    public int TurnAttacks = 0;

    // Effects //
    public bool Frozen = false;
    public bool Immune = false;
    public bool Forgetful = false;

    #region Constructor

    private Hero() { }

    public static Hero Create(Player player, HeroClass heroClass)
    {
        Hero hero = new Hero()
        {
            Player = player,
            Class = heroClass
        };

        hero.Initialize();

        return hero;
    }

    private void Initialize()
    {
        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
        Armor = 0;
    }

    #endregion


    public void Attack(ICharacter target)
    {
        // Checking if Hero is forgetful
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
        HeroPreAttackEvent heroPreAttackEvent = EventManager.Instance.OnHeroPreAttack(this, target, this.CurrentAttack);

        // Checking if the Attack was cancelled
        if (heroPreAttackEvent.Status != PreStatus.Cancelled)
        {
            // Adding 1 to the turn attacks counter
            TurnAttacks++;

            // Redefining target in case it changed when firing events
            target = heroPreAttackEvent.Target;

            // Target is a Hero
            if (target.IsHero())
            {
                target.As<Hero>().TryDamage(this, this.GetHeroAttack());
            }

            // Target is a Minion
            else if (target.IsMinion())
            {
                // Casting ICharacter to MinionCard
                MinionCard targetMinion = target.As<MinionCard>();

                // Getting the minion attack
                int minionAttack = targetMinion.CurrentAttack;
                
                // Damaging both characters
                this.TryDamage(targetMinion, minionAttack);
                targetMinion.TryDamage(this, this.GetHeroAttack());

                // Checking the death of both characters
                this.CheckDeath();
                targetMinion.CheckDeath();
            }

            // Firing OnAttacked events
            EventManager.Instance.OnHeroAttacked(this, target);
        }
    }

    public void TryDamage(ICharacter attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, attacker, damageAmount);

        if (attacker.IsAlive())
        {
            this.Damage(heroPreDamageEvent.Damage);

            EventManager.Instance.OnHeroDamaged(this, attacker, heroPreDamageEvent.Damage);
        }
    }

    public void Damage(int damageAmount)
    {
        this.CurrentHealth -= damageAmount;

        // TODO : Sprite -> Show health loss on hero portrait
    }

    public void Heal(int healAmount)
    {
        // Firing OnHeroPreHeal events 
        HeroPreHealEvent heroPreHealEvent = EventManager.Instance.OnHeroPreHeal(this, healAmount);

        // Calculating the healeable health
        int healeableHealth = MaxHealth - CurrentHealth;

        if (heroPreHealEvent.HealAmount > healeableHealth)
        {
            this.CurrentHealth = MaxHealth;
        }
        else
        {
            this.CurrentHealth += heroPreHealEvent.HealAmount;
        }

        // Firing OnHeroHealed events 
        EventManager.Instance.OnHeroHealed(this, heroPreHealEvent.HealAmount);

        // TODO : Heal animation
        // TODO : Show heal sprite + healed amount
    }

    public void CheckDeath()
    {
        if (this.IsAlive() == false)
        {
            // TODO : End game
        }
    }

    public bool CanAttack()
    {
        switch (TurnAttacks)
        {
            case 0:
                return (this.CurrentAttack > 0 || this.Player.HasWeapon());

            case 1:
                return (this.Player.HasWeapon() && this.Player.Weapon.Windfury);

            default:
                return false;
        }
    }

    public bool CanTarget(ICharacter target)
    {
        if (target.IsHero())
        {
            return (target.As<Hero>().Player.HasTauntMinions() == false);
        }
        else
        {
            MinionCard minionTarget = target.As<MinionCard>();

            if (minionTarget.Taunt == false)
            {
                return (minionTarget.Player.HasTauntMinions() == false);
            }
            else
            {
                return true;
            }
        }
    }

    public int GetHeroAttack()
    {
        int attack = this.CurrentAttack;

        if (this.Player.HasWeapon())
        {
            attack += this.Player.Weapon.CurrentAttack;
        }

        return attack;
    }
}