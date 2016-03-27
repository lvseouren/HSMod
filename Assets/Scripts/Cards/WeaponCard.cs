public class WeaponCard : BaseCard
{
    public int BaseAttack;
    public int CurrentAttack;
    public int Durability;

    public bool Windfury;
    public bool Forgetful;

    #region Events

    public virtual void OnPlayed()
    {

    }

    public virtual void OnPreAttack()
    {

    }

    public virtual void OnAttacked()
    {

    }

    public virtual void OnDestroyed()
    {

    }

    #endregion

    #region Methods

    public void Attack(MinionCard targetMinion)
    {
        
    }

    public void Attack(Hero targetHero)
    {
        
    }

    public void Equip()
    {
        
    }

    public void Destroy()
    {
        
    }

    #endregion
}