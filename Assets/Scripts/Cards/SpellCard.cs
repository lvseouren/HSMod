public class SpellCard : BaseCard
{
    public TargetType TargetType;

    public virtual void Cast()
    {
        
    }

    public virtual void Cast(ICharacter target)
    {
        
    }
}

public enum TargetType
{
    NoTarget, // No target, such as Innervate
    TargetAll, // Can target all (heros and minions), such as Healing Touch
    TargetAllMinions, // Can target all minions (friendly and enemy minions), such as Mark of Nature
    TargetAllEnemies, // Can target all enemies (hero and minions), such as Swipe
    TargetEnemyMinions, // Can target all enemy minions, such as Wrath
    TargetFriendlyMinions // Can target all friendly minions, such as Shadowflame
}