using System;

public class Ghoul : MinionCard
{
    public Ghoul()
    {
        Name = "Ghoul";
        Description = "Charge. At the end of your turn destroy this minion.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;
        MinionType = MinionType.General;

        BaseCost = 1;
        BaseAttack = 1;
        BaseHealth = 1;

        Charge = true;

        this.BuffManager.OnTurnEnd.Subscribe(x => this.OnTurnEnd());
    }

    public void OnTurnEnd()
    {
        this.Die();
    }
}