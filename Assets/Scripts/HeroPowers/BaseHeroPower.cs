public abstract class BaseHeroPower
{
    public string Name;
    public string Description;
    public HeroClass Class;
    public TargetType TargetType;

    public int BaseCost;
    public int CurrentCost;

    public Hero Hero;

    public virtual void Use() { }

    public virtual void Upgrade() { }
}