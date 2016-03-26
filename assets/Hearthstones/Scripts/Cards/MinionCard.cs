public class MinionCard : BaseCard
{
    public int BaseAttack;
    public int CurrentAttack;
    public int Health;
    public MinionType MinionType;
    public bool Taunt;
    public bool Charge;
    public bool Stealth;
    public bool DivineShield;
    public bool Elusive;
    public bool Forgetful;
    public int SpellDamage;

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

    #endregion

    #region Methods

    public virtual void AddBuff(object buff)
    {
        
    }

    public virtual void Attack(MinionCard otherMinion)
    {
        
    }

    public virtual void Attack(Hero hero)
    {
        
    }

    public virtual void Die()
    {
        
    }

    public virtual void Destroy()
    {
        
    }

    #endregion
}

public enum MinionType
{
    General,
    Murloc,
    Beast,
    Mech,
    Dragon,
    Pirate,
    Demon,
    Totem
}