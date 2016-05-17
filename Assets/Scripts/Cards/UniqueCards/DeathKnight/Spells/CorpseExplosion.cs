using System;

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
        target.As<Minion>().Buffs.Deathrattle.Subscribe(x => ExplodeCorpse(x));
    }

    public void ExplodeCorpse(Minion corpse)
    {
        // Iterating on the list of enemy minions to damage them
        foreach (Minion minion in corpse.Player.Enemy.Minions)
        {
            minion.TryDamage(null, 2);
        }

        // Iterating on the list of enemy minions to check if they should die
        foreach (Minion minion in corpse.Player.Enemy.Minions)
        {
            minion.CheckDeath();
        }
    }
}