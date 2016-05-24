public class ArcaneAnomaly : MinionCard
{
    public ArcaneAnomaly()
    {
        Name = "Arcane Anomaly";
        Description = "Whenever you spend mana, deal 1 damage to a random enemy.";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Rare;
        MinionType = MinionType.General;

        BaseCost = 2;
        BaseAttack = 2;
        BaseHealth = 1;

        InitializeMinion();
    }

    // TODO
}