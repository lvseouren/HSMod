using System;

public class SkeletonCommander : MinionCard
{
    public IDisposable MinionPlayedSubscription;

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

        this.BuffManager.Battlecry.Subscribe(x => this.Battlecry());
    }

    public void Battlecry()
    {
        MinionPlayedSubscription = EventManager.Instance.MinionPlayedHandler.Subscribe(x => UndeadBuff(x));
    }

    public void UndeadBuff(MinionPlayedEvent minionPlayedEvent)
    {
        if (minionPlayedEvent.Player == this.Player && minionPlayedEvent.Minion.MinionType == MinionType.Undead)
        {
            minionPlayedEvent.Minion.AddBuff(new SkeletonCommanderBuff());

            this.MinionPlayedSubscription.Dispose();
        }
    }
}

public class SkeletonCommanderBuff : BaseBuff
{
    public SkeletonCommanderBuff()
    {
        Name = "Commanded";
        Description = "+1/+1";

        BuffType = BuffType.Buff;
    }

    public override void OnAdded(MinionCard minion)
    {
        minion.CurrentAttack += 1;

        minion.MaxHealth += 1;
        minion.CurrentHealth += 1;
    }

    public override void OnRemoved(MinionCard minion)
    {
        minion.CurrentAttack -= 1;

        minion.MaxHealth -= 1;
        minion.CurrentHealth -= 1;
    }
}