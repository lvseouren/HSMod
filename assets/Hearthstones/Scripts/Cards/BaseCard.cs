public abstract class BaseCard
{
    public string Name;
    public string Description;
    public int Cost;
    public Class Class;
    public Rarity Rarity;
    public bool Combo;

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

public enum Class
{
    Druid,
    Hunter,
    Mage,
    Paladin,
    Priest,
    Rogue,
    Shaman,
    Warlock,
    Warrior,
    DeathKnight,
    Monk,
    DemonHunter
}

public enum Rarity
{
    Basic,
    Common,
    Rare,
    Epic,
    Legendary
}