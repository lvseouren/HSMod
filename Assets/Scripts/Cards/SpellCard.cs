public class SpellCard : BaseCard
{
    public TargetType TargetType;
    
    public void OnCast(ICharacter target)
    {
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this.Player, this);

        if (spellPreCastEvent.IsCancelled == false)
        {
            Cast(target);
        }

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }

    public virtual void Cast(ICharacter target) { }

    public virtual bool CanTarget(ICharacter target)
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
    NoTarget, // No target, such as Innervate (Druid)
    AllCharacters, // Can target all (heros and minions), such as Healing Touch (Druid)
    AllMinions, // Can target all minions (friendly and enemy), such as Inner Rage (Warrior)
    EnemyCharacters, // Can target all enemy characters (hero and minions), such as Swipe (Druid)
    EnemyMinions, // Can target enemy minions, such as Wrath (Druid)
    FriendlyCharacters, //Can target friendly minions and own hero, such as Rockbiter Weapon (Shaman)
    FriendlyMinions // Can target friendly minions, such as Shadowflame (Warlock)
}