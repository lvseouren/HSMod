public class SpitefulWrath : MinionCard
{
    public SpitefulWrath()
    {
        Name = "Spiteful Wrath";
        Description = "";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 1;
        BaseAttack = 2;
        BaseHealth = 1;

        InitializeMinion();
    }
}