public enum TargetType
{
    NoTarget, // No target, such as Innervate (Druid)
    AllCharacters, // Can target all characters (heros and minions), such as Healing Touch (Druid)
    AllMinions, // Can target all minions (friendly and enemy), such as Inner Rage (Warrior)
    EnemyCharacters, // Can target all enemy characters (hero and minions), such as Swipe (Druid)
    EnemyMinions, // Can target enemy minions, such as Wrath (Druid)
    FriendlyCharacters, //Can target friendly minions and own hero, such as Rockbiter Weapon (Shaman)
    FriendlyMinions // Can target friendly minions, such as Shadowflame (Warlock)
}