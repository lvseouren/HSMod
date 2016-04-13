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
            if (target is Hero)
            {
                // Casting ICharacter to Hero
                Hero heroTarget = (Hero) target;

                // Firing OnHeroPreDamage event
                HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(heroTarget, this, this.CurrentAttack);

                // Checking if the Attack was cancelled
                if (heroPreDamageEvent.IsCancelled == false)
                {
                    // Attacking the target hero
                    heroTarget.Damage(heroPreDamageEvent.Damage);

                    // Firing OnHeroDamaged event
                    EventManager.Instance.OnHeroDamaged(this, heroTarget, heroPreDamageEvent.Damage);
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
                    int heroAttack = this.CurrentAttack;
                    int minionAttack = minionTarget.CurrentAttack;

                    // Damaging both hero and minion
                    minionTarget.Damage(heroAttack);
                    this.Damage(minionAttack);

                    // Triggering specific and global events for the minion
                    minionTarget.BuffManager.OnDamaged.OnNext(null);
                    EventManager.Instance.OnMinionDamaged(this, minionTarget);

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
                this.CurrentHealth -= heroPreDamageEvent.Damage;
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