public class BladeOfLostSouls : WeaponCard
{
    public BladeOfLostSouls()
    {
        Name = "Blade of Lost Souls";
        Description = "Deathrattle: Draw a card.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        BaseCost = 3;
        BaseAttack = 3;
        BaseDurability = 2;
    }

    public override void OnDestroyed()
    {
        this.Player.Deck.Draw();
    }
}