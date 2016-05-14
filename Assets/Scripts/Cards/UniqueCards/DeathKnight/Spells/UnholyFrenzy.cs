public class UnholyFrenzy : SpellCard
{
    public UnholyFrenzy()
    {
        Name = "Unholy Frenzy";
        Description = "Deal 1 damage to a minion. Give it +4 Attack";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        TargetType = TargetType.AllMinions;

        BaseCost = 1;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        int damage = 1 + Player.GetSpellPower();

        target.TryDamage(null, damage);

        target.As<MinionCard>().AddBuff(new UnholyFrenzyBuff());
    }
}

public class UnholyFrenzyBuff : BaseBuff
{
    public override void OnAdded(Minion minion)
    {
        minion.CurrentAttack += 4;
    }

    public override void OnRemoved(Minion minion)
    {
        minion.CurrentAttack -= 4;
    }
}