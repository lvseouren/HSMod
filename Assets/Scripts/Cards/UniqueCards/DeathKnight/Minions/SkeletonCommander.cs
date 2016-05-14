using System;

public class SkeletonCommander : MinionCard
{
    public IDisposable MinionPlayedSubscription;

    public SkeletonCommander()
    {
        Name = "Skeleton Commander";
        Description = "Deathrattle: The next Undead you play gets +1/+1.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 1;
        BaseAttack = 1;
        BaseHealth = 1;

        Buffs.Battlecry.Subscribe(x => Battlecry());

        InitializeMinion();
    }

    public void Battlecry()
    {
        MinionPlayedSubscription = EventManager.Instance.MinionPlayedHandler.Subscribe(x => UndeadBuff(x));
    }

    public void UndeadBuff(MinionPlayedEvent minionPlayedEvent)
    {
        if (minionPlayedEvent.Player == Player && minionPlayedEvent.Minion.Card.MinionType == MinionType.Undead)
        {
            minionPlayedEvent.Minion.AddBuff(new SkeletonCommanderBuff());

            MinionPlayedSubscription.Dispose();
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

    public override void OnAdded(Minion minion)
    {
        minion.CurrentAttack += 1;

        minion.MaxHealth += 1;
        minion.CurrentHealth += 1;
    }

    public override void OnRemoved(Minion minion)
    {
        minion.CurrentAttack -= 1;

        minion.MaxHealth -= 1;
        minion.CurrentHealth -= 1;
    }
}