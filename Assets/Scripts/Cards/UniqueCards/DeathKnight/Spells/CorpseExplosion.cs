using System;
using System.Collections.Generic;

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
        target.As<Minion>().Buffs.Deathrattle.Subscribe(x => ExplodeCorpse());
    }

    public void ExplodeCorpse()
    {
        // Getting a reference to the list of enemy minions
        List<Minion> enemyMinions = Player.Enemy.Minions;

        // Iterating on the list to damage the minions
        foreach (Minion minion in enemyMinions)
        {
            minion.TryDamage(null, 2);
        }

        // Iterating on the list to check if the minions should die
        foreach (Minion minion in enemyMinions)
        {
            minion.CheckDeath();
        }
    }
}