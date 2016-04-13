public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        BaseCost = 2;
    }

    // TODO : Move this to base class somehow
    public override void Cast(ICharacter target)
    {
        int damage = 2 + this.Player.GetSpellPower();

        if (target.IsHero())
        {
            target.As<Hero>().TryDamage(null, damage);
        }
        else if (target.IsMinion())
        {
            target.As<MinionCard>().TryDamage(null, damage);
        }

        // TODO : Summon 1/1
    }
}