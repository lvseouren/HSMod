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
        BaseAttack = 1;
        BaseHealth = 2;

        Buffs.Deathrattle.Subscribe(Deathrattle);

        InitializeMinion();
    }

    public void Deathrattle(Minion deadMinion)
    {
        MinionCard spitefulWrath = new SpitefulWrath();
        spitefulWrath.SetOwner(deadMinion.Player);

        deadMinion.Player.SummonMinion(spitefulWrath);
    }
}