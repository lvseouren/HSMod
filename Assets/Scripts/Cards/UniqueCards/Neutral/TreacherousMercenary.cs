﻿public class TreacherousMercenary : MinionCard
{
    public TreacherousMercenary()
    {
        Name = "Treacherous Mercenary";
        Description = "Charge. Battlecry: Deal 3 damage to your hero.";

        CardClass = CardClass.Neutral;
        Rarity = Rarity.Rare;
        MinionType = MinionType.General;

        Cost = 3;
        BaseAttack = 4;
        CurrentAttack = 4;
        Health = 2;

        Charge = true;
    }

    public override void OnPlayed()
    {
        // TODO : Deal 3 damage to your hero.
    }
}