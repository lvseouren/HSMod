using UnityEngine;

public class Hero : MonoBehaviour, ICharacter
{
    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Armor = 0;

    // Effects //
    public bool Frozen = false;
    public bool Immune = false;
    public bool Forgetful = false;

    private void Start()
    {

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
                HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(this, heroTarget);

                // Checking if the Attack was cancelled
                if (heroPreDamageEvent.IsCancelled == false)
                {
                    // Getting the atttack values
                    int heroAttack = this.CurrentAttack;

                    // Attacking the target hero
                    heroTarget.Damage(this.CurrentAttack);

                    // Firing OnHeroDamaged event
                    EventManager.Instance.OnHeroDamaged(this, heroTarget, heroAttack);
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
                    minionTarget.OnDamaged(this, heroAttack);
                    EventManager.Instance.OnMinionDamaged(this, minionTarget);

                    // Triggering global events for the hero
                    EventManager.Instance.OnHeroDamaged(minionTarget, this, minionAttack);

                    // Checking death of the minion
                    minionTarget.CheckDie();
                }
            }

            // Firing OnAttacked events
            EventManager.Instance.OnHeroAttacked(this, target);
        }
    }

    public void Damage(int damageAmount)
    {
        this.CurrentHealth -= damageAmount;
        // TODO : Sprite -> Show health loss on hero portrait
    }
}