using System;

public class TreacherousMercenary : MinionCard
{
    public TreacherousMercenary()
    {
        Name = "Treacherous Mercenary";
        Description = "Charge. Battlecry: Deal 3 damage to your hero.";

        CardClass = CardClass.Neutral;
        Rarity = Rarity.Rare;
        MinionType = MinionType.General;

        BaseCost = 3;
        BaseAttack = 4;
        BaseHealth = 2;

        Charge = true;

        this.BuffManager.Battlecry.Subscribe(x => this.Battlecry());
    }

    public void Battlecry()
    {
        this.Player.Hero.Damage(3);
    }
}