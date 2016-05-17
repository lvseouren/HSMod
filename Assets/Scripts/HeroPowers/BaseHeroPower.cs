public abstract class BaseHeroPower
{
    #region Base Stats

    public string Name;
    public string Description;
    public HeroClass Class;
    public TargetType TargetType;
    public int BaseCost;
    public bool Golden = false;

    #endregion

    #region In-game Stats
    
    public Hero Hero;
    public HeroPowerController Controller;

    public int CurrentCost;
    public int MaxUses = 1;
    public int CurrentUses;

    #endregion

    #region Constructor

    public void Initialize()
    {
        CurrentCost = BaseCost;
        CurrentUses = 0;
        
        Controller = HeroPowerController.Create(this);
    }

    #endregion

    #region Methods
    
    public virtual void Use(Character target) { }

    public virtual void Upgrade() { }

    #endregion

    #region Condition Checkers

    public bool IsAvailable()
    {
        return (CurrentUses < MaxUses) && (CurrentCost <= Hero.Player.AvailableMana);
    }

    public virtual bool CanTarget(Character target)
    {
        return target.IsElusive == false;
    }

    #endregion
}