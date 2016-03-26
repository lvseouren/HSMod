public class SpellCard : BaseCard
{
    public TargetType TargetType;

    public void Cast()
    {
        
    }

    public void Cast(MinionCard targetMinion)
    {

    }

    public void Cast(Hero targetHero)
    {

    }
}

public enum TargetType
{
    NoTarget,
    TargetAll,
    TargetAllMinions,
    TargetEnemyMinions,
    TargetFriendlyMinions
}