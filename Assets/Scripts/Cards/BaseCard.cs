public abstract class BaseCard
{
    // Base Stats //
    public string Name;
    public string Description;
    public int BaseCost;
    public HeroClass Class;
    public CardRarity Rarity;
    public bool Golden = false;

    // In-Game Stats //
    public Player Player;
    public int CurrentCost;
    public BaseController Controller;

    // Effects //
    public int Overload = 0;
    public bool Combo = false;

    // TODO : Image, Voices, Effects?

    #region Hand/Deck Events

    public virtual void OnDrawn()
    {
        
    }

    public virtual void OnDiscarded()
    {
        
    }

    #endregion
}