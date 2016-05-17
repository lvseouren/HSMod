public class Necrosis : SpellCard
{
    public Necrosis()
    {
        Name = "Necrosis";
        Description = "Deal 4 damage. Costs (1) less for each minion that died this turn.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Rare;

        TargetType = TargetType.AllCharacters;

        BaseCost = 4;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        int damage = 4 + Player.GetSpellPower();

        target.Damage(null, damage);
    }
}