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
        HeroPreAttackEvent heroPreAttackEvent = EventManager.Instance.OnHeroPreAttack(this, target);

        // Checking if the Attack was cancelled
        if (heroPreAttackEvent.IsCancelled)
        {
            // Checking the target type
            if (target.IsHero())
            {
                target.As<Hero>().TryDamage(this, this.CurrentAttack);
            }
            else if (target.IsMinion())
            {
                // Casting ICharacter to MinionCard
                MinionCard minionTarget = target.As<MinionCard>();

                // Firing OnMinionPreDamage event
                MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(minionTarget, this, this.CurrentAttack);

                // Checking if the attack was cancelled
                if (minionPreDamageEvent.IsCancelled == false)
                {
                    // TODO : Animation

                    // Damaging both hero and minion
                    this.Damage(minionTarget.CurrentAttack);
                    minionTarget.TryDamage(this, minionPreDamageEvent.Damage);

                    // Triggering specific and global events for the minion
                    minionTarget.BuffManager.OnDamaged.OnNext(null);
                    EventManager.Instance.OnMinionDamaged(minionTarget, this, minionPreDamageEvent.Damage);

                    // Triggering global events for the hero
                    EventManager.Instance.OnHeroDamaged(this, minionTarget, minionAttack);

                    // Checking death of the minion
                    minionTarget.CheckDeath();
                }
            }

            // Firing OnAttacked events
            EventManager.Instance.OnHeroAttacked(this, target);
        }
    }

    public void TryDamage(ICharacter attacker, int damageAmount)
    {
        HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, attacker, damageAmount);

        if (heroPreDamageEvent.IsCancelled == false)
        {
            if (attacker.IsAlive())
            {
                this.Damage(damageAmount);
            }

            EventManager.Instance.OnHeroDamaged(this, attacker, damageAmount);
        }
    }

    public void Damage(int damageAmount)
    {
        this.CurrentHealth -= damageAmount;
        // TODO : Sprite -> Show health loss on hero portrait
    }

    public void Heal(int healAmount)
    {
        // TODO : OnHeroPreHeal

        int healeableHealth = MaxHealth - CurrentHealth;

        if (healAmount > healeableHealth)
        {
            this.CurrentHealth = MaxHealth;
        }
        else
        {
            this.CurrentHealth += healAmount;
        }

        // Firing OnHeroHealed events 
        EventManager.Instance.OnHeroHealed(this, healAmount);

        // TODO : Show heal animation + healed amount
    }
}