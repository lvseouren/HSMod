public class SpellCard : BaseCard
{
    public TargetType TargetType;

    public void InitializeSpell()
    {
        InitializeCard();
    }

    public virtual void Cast(Character target) { }

    public virtual bool CanTarget(Character target)
    {
        if (target != null)
        {
            // The target is a Hero
            if (target.IsHero())
            {
                // The target is the own Hero
                if (Player.Hero.IsFriendlyOf(target))
                {
                    // True for AllCharacters
                    return (TargetType == TargetType.AllCharacters || TargetType == TargetType.FriendlyCharacters);
                }

                // The target is the enemy Hero
                else
                {
                    // True for AllCharacters and EnemyCharacters types
                    return (TargetType == TargetType.AllCharacters || TargetType == TargetType.EnemyCharacters);
                }
            }

            // The target is a Minion
            else
            {
                // The target is friendly
                if (Player.Hero.IsFriendlyOf(target))
                {
                    return (TargetType == TargetType.AllCharacters || TargetType == TargetType.AllMinions || TargetType == TargetType.FriendlyMinions);
                }

                // The target is enemy
                else
                {
                    return (TargetType == TargetType.AllCharacters || TargetType == TargetType.AllMinions || TargetType == TargetType.EnemyMinions);
                }
            }
        }

        return false;
    }

    public void PlayOn(Character target)
    {
        Player.UseMana(CurrentCost);

        Player.PlaySpell(this, target);
    }
}