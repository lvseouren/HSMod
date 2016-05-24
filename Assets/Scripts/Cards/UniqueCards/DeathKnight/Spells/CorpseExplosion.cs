using System;
using System.Linq;

public class CorpseExplosion : SpellCard
{
    public CorpseExplosion()
    {
        Name = "Corpse Explosion";
        Description = "Give a minion Deathrattle: Deal 2 damage to all enemy minions.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        TargetType = TargetType.AllMinions;

        BaseCost = 2;

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        target.As<Minion>().Buffs.Deathrattle.Subscribe(Deathrattle);
    }

    public void Deathrattle(Minion deadMinion)
    {
        // Iterating on the list of enemy minions to damage them
        foreach (Minion minion in deadMinion.Player.Enemy.Minions)
        {
            minion.Damage(null, 2);
        }

        // Iterating on the list of enemy minions to check if they should die
        foreach (Minion minion in deadMinion.Player.Enemy.Minions.ToList())
        {
            minion.CheckDeath();
        }
    }
}