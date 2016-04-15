using System;

public class VengefulSpirit : MinionCard
{
    public VengefulSpirit()
    {
        Name = "Vengeful Spirit";
        Description = "Deathrattle: Summon a 2/1 Spiteful Wraith.";

        Class = HeroClass.Neutral;
        Rarity = CardRarity.Common;
        MinionType = MinionType.Undead;

        BaseCost = 2;
        BaseAttack = 2;
        BaseHealth = 2;

        this.BuffManager.Deathrattle.Subscribe(x => this.Deathrattle(x));
    }

    public void Deathrattle(MinionCard x)
    {
        // TODO : Spawn 2/1 Spiteful Wrath
    }
}