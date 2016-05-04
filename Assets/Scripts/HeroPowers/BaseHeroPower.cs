public abstract class BaseHeroPower
{
    // Base Stats //
    public string Name;
    public string Description;
    public HeroClass Class;
    public TargetType TargetType;
    public int BaseCost = 2;
    public bool Golden = false;

    // In-Game Stats //
    public Hero Hero;
    public int CurrentCost = 2;
    public int MaxUses = 1;
    public int CurrentUses = 0;

    public virtual void Use() { }

    public virtual void Use(Character target) { }

    public virtual void Upgrade() { }

    public virtual bool CanTarget(Character target)
    {
        return true;
    }

    public bool IsAvailable()
    {
        if (this.CurrentUses < this.MaxUses)
        {
            if (this.CurrentCost < this.Hero.Player.AvailableMana)
            {
                return true;
            }
        }

        return false;
    }
}