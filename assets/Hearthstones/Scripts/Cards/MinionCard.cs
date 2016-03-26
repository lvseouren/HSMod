public class MinionCard : BaseCard
{
    public int BaseAttack;
    public int CurrentAttack;
    public int Health;
    public MinionType MinionType;

    public void Attack(MinionCard other)
    {
        
    }

    public void Attack(Hero hero)
    {
        
    }

    public void Die()
    {
        
    }

    public void Destroy()
    {
        
    }
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