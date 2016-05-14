using System;

public class Ghoul : MinionCard
{
    public Ghoul()
    {
        Name = "Ghoul";
        Description = "Charge. At the end of your turn destroy this minion.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;
        MinionType = MinionType.General;

        BaseCost = 1;
        BaseAttack = 1;
        BaseHealth = 1;

        HasCharge = true;

        InitializeMinion();
    }

    public void Initialize()
    {
        Buffs.OnTurnEnd.Subscribe(x => OnTurnEnd());
    }

    public void OnTurnEnd()
    {
        Minion.Die();
    }
}