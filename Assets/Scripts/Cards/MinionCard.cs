public class MinionCard : BaseCard, IDamageable
{
    // Stats //
    public int BaseAttack;
    public int CurrentAttack;
    public int Health;
    public MinionType MinionType;

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

    #region Events

    public virtual void OnPlayed()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnDamaged()
    {

    }

    public virtual void OnDead()
    {

    }

    public virtual void OnSelectedBySpell()
    {

    }

    public virtual void OnPassiveAbilityUsed()
    {

    }

    public virtual void OnTurnEnd()
    {
        
    }

    #endregion

    #region Methods

    public virtual void AddBuff(object buff)
    {
        // TODO : Buff list + buff class probably
    }

    public virtual void Attack(IDamageable target)
    {
        // TODO : Attack stuff

        //EventManager.OnMinionAttacked(this, target, damage);
    }

    public virtual void Damage(int damageAmount)
    {
        this.Health -= damageAmount;

        if (this.Health < 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy();
    }

    public virtual void Destroy()
    {
        // TODO : Play destroy animation (dust and stuff)

        Destroy(this.gameObject);
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