public class BladeOfLostSouls : WeaponCard
{
    public BladeOfLostSouls()
    {
        Name = "Blade of Lost Souls";
        Description = "Deathrattle: Draw a card.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        BaseCost = 3;
        BaseAttack = 3;
        BaseDurability = 2;
    }

    public override void Deathrattle()
    {
        this.Player.Draw();
    }
}