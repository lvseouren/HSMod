public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        TargetType = TargetType.AllCharacters;

        BaseCost = 2;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        int damage = 2 + Player.GetSpellPower();

        target.TryDamage(null, damage);

        // TODO : Summon 1/1
    }
}