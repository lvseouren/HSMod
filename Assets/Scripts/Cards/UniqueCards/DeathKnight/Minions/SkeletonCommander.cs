using System;

public class SkeletonCommander : MinionCard
{
    public SkeletonCommander()
    {
        Name = "Skeleton Commander";
        Description = "Deathrattle: The next Undead you play gets +1/+1.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 1;
        BaseAttack = 1;
        BaseHealth = 1;
    }

    public override void OnPlayed()
    {
        EventManager.Instance.MinionPlayedHandler.Subscribe(UndeadBuff);
    }

    public void UndeadBuff(MinionPlayedEvent minionPlayedEvent)
    {
        if (minionPlayedEvent.Player == this.Player && minionPlayedEvent.Minion.MinionType == MinionType.Undead)
        {
            // TODO : Add +1/+1 buff.
        }
    }
}