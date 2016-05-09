public abstract class BaseHeroPower
{
    // Base Stats //
    public string Name;
    public string Description;
    public HeroClass Class;
    public TargetType TargetType;
    public int BaseCost;
    public bool Golden = false;

    // In-Game Stats //
    public Hero Hero;
    public HeroPowerController Controller;

    public int CurrentCost;
    public int MaxUses = 1;
    public int CurrentUses;

    #region Constructor

    public void Initialize()
    {
        CurrentCost = BaseCost;
        CurrentUses = 0;
        
        Controller = HeroPowerController.Create(this);
    }

    #endregion

    #region Methods

    public virtual void Use() { }

    public virtual void Use(Character target) { }

    public virtual void Upgrade() { }

    #endregion

    #region Condition Checkers

    public virtual bool CanTarget(Character target)
    {
        return true;
    }

    public virtual bool IsAvailable()
    {
        return CurrentUses < MaxUses;
    }

    #endregion
}