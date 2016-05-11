using System;

public class DancingRuneblade : MinionCard
{
    public DancingRuneblade()
    {
        Name = "Dancing Runeblade";
        Description = "Battlecry: Gain Attack and Health equal to your weapon's Attack and Durability.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Rare;
        MinionType = MinionType.Undead;

        BaseCost = 1;
        BaseAttack = 1;
        BaseHealth = 1;

        Buffs.Battlecry.Subscribe(x => Battlecry());
    }

    public void Battlecry()
    {
        // TODO : Gain Attack and Health equal to your weapon's Attack and Durability
    }
}
