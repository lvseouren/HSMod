public class RaiseDead : SpellCard
{
    public RaiseDead()
    {
        Name = "Raise Dead";
        Description = "Summon a 3/3 Ghoul. Costs (1) less for each minion that died this turn.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        TargetType = TargetType.NoTarget;

        BaseCost = 3;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        MinionCard ghoul = new Ghoul()
        {
            BaseAttack = 3,
            BaseHealth = 3
        };
        ghoul.SetOwner(Player);

        Player.SummonMinion(ghoul);
    }
}