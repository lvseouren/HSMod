public class SpellCard : BaseCard
{
    public TargetType TargetType;

    public virtual void Cast()
    {

    }

    public virtual void Cast(ICharacter target)
    {
        if (this.CanTarget(target))
        {
            
        }
    }

    public bool CanTarget(ICharacter target)
    {
        // The target is a Hero
        if (target.IsHero())
        {
            // The target is the own Hero
            if (this.Player.Hero == target.As<Hero>())
            {
                // True for AllCharacters
                return this.TargetType == TargetType.AllCharacters;
            }

            // The target is the enemy Hero
            else
            {
                // True for AllCharacters and EnemyCharacters types
                return (this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.EnemyCharacters);
            }
        }

        // The target is a Minion
        else
        {
            // The target is friendly
            if (this.Player == target.As<MinionCard>().Player)
            {
                return (this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.FriendlyMinions);
            }

            // The target is enemy
            else
            {
                return (this.TargetType == TargetType.AllCharacters || this.TargetType == TargetType.AllMinions || this.TargetType == TargetType.EnemyMinions);
            }
        }
    }
}

public enum TargetType
{
    NoTarget, // No target, such as Innervate
    AllCharacters, // Can target all (heros and minions), such as Healing Touch
    AllMinions, // Can target all minions (friendly and enemy minions), such as Mark of Nature
    EnemyCharacters, // Can target all enemies (hero and minions), such as Swipe
    EnemyMinions, // Can target all enemy minions, such as Wrath
    // Does FriendlyCharacters exist ??
    FriendlyMinions // Can target all friendly minions, such as Shadowflame
}