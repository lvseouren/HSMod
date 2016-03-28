public class SkeletonCommander : MinionCard
{
    public SkeletonCommander()
    {
        Name = "Skeleton Commander";
        Description = "Deathrattle: The next Undead you play gets +1/+1.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;
        MinionType = MinionType.Undead;

        Cost = 1;
        BaseAttack = 1;
        CurrentAttack = 1;
        BaseHealth = 1;
    }

    public override void OnDead()
    {
        // TODO : The next Undead you play gets +1/+1.
    }
}