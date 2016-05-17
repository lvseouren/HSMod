public abstract class BaseCard
{
    #region Base Stats

    public string Name;
    public string Description;
    public int BaseCost;
    public HeroClass Class;
    public CardRarity Rarity;
    public bool Golden = false;

    #endregion

    #region In-Game Stats

    public Player Player;
    public CardController Controller;

    public int CurrentCost;

    #endregion

    #region In-Game Effects

    public bool Combo = false;
    public int Overload = 0;

    #endregion

    // TODO : Image, Voices, Effects?

    public void InitializeCard()
    {
        CurrentCost = BaseCost;
    }

    #region Methods
    
    public virtual void Play() { }

    public virtual void Discard()
    {
        // TODO
    }

    public void SetOwner(Player player)
    {
        Player = player;
    }

    #endregion

    #region Hand/Deck Events

    public virtual void OnDrawn()
    {
        
    }

    public virtual void OnDiscarded()
    {
        
    }

    #endregion
}