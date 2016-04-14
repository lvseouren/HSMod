using UnityEngine;

public class Hero : MonoBehaviour, ICharacter
{
    public Player Player;

    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Armor;

    // Effects //
    public bool Frozen = false;
    public bool Immune = false;
    public bool Forgetful = false;

    private void Start()
    {
        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
        Armor = 0;
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
        HeroPreAttackEvent heroPreAttackEvent = EventManager.Instance.OnHeroPreAttack(this, target, this.CurrentAttack);

        // Checking if the Attack was cancelled
        if (heroPreAttackEvent.Status != PreStatus.Cancelled)
        {
            // Redefining target in case it changed when firing events
            target = heroPreAttackEvent.Target;

            // Target is a Hero
            if (target.IsHero())
            {
                target.As<Hero>().TryDamage(this, this.CurrentAttack);
            }

            // Target is a Minion
            else if (target.IsMinion())
            {
                // Casting ICharacter to MinionCard
                MinionCard minionTarget = target.As<MinionCard>();

                // Getting the minion attack
                int minionAttack = minionTarget.CurrentAttack;

                // Damaging the minion
                minionTarget.TryDamage(this, this.CurrentAttack);

                // Damaging the hero
                this.TryDamage(minionTarget, minionAttack);

                minionTarget.CheckDeath();
                this.CheckDeath();
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
}