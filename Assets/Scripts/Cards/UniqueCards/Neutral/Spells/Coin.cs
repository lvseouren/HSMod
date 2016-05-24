public class Coin : SpellCard
{
    public Coin()
    {
        Name = "Coin";
        Description = "Gain 1 Mana Crystal this turn only.";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Basic;

        BaseCost = 0;

        TargetType = TargetType.NoTarget;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        Player.AddMana(1);
    }
}