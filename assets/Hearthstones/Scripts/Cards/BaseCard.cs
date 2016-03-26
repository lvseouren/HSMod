public class BaseCard
{
    public string Name;
    public string Description;
    public int Cost;
    public Class Class;
    public Rarity Rarity;

    // Image ?
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