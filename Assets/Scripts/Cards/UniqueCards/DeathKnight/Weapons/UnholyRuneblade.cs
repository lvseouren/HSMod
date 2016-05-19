public class UnholyRuneblade : WeaponCard
{
    public UnholyRuneblade()
    {
        Name = "Unholy Runeblade";
        Description = "Battlecry: Silence a minion.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Basic;

        BaseCost = 4;
        BaseAttack = 4;
        BaseDurability = 2;

        InitializeWeapon();
    }
    
    public override void Battlecry()
    {
        // TODO : Silence a minion
    }
}