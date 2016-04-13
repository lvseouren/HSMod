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
    }

    public override void Cast(ICharacter target)
    {
        target.As<MinionCard>().BuffManager.Deathrattle.Subscribe(x => ExplodeCorpse());
    }

    public void ExplodeCorpse()
    {
        // Getting a reference to the list of enemy minions
        List<MinionCard> enemyMinions = this.Player.Enemy.Minions;

        // Iterating on the list to damage the minions
        foreach (MinionCard minion in enemyMinions)
        {
            // TODO : PreDamage ?

            // Damaging the minion
            minion.Damage(2);


            EventManager.Instance.OnMinionDamaged(null, minion);
        }

        // Iterating on the list to check the minions
        foreach (MinionCard minion in enemyMinions)
        {
            // Checking if the minion should die
            minion.CheckDeath();
        }
    }
}