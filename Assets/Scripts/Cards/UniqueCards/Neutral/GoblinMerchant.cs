public class GoblinMerchant : MinionCard
{
    public GoblinMerchant()
    {
        Name = "Goblin Merchant";
        Description = "Your hero power costs (1).";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Rare;
        MinionType = MinionType.General;

        BaseCost = 2;
        BaseAttack = 2;
        BaseHealth = 2;

        InitializeMinion();
    }

    // TODO
}