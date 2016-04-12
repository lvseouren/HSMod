public class CorpseExplosion : SpellCard
{
    public CorpseExplosion()
    {
        Name = "Corpse Explosion";
        Description = "Give a minion Deathrattle: Deal 2 damage to all enemy minions.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        TargetType = TargetType.AllMinions;

        BaseCost = 2;
    }

    public override void Cast(ICharacter target)
    {
        //EventManager.OnSpellPreCast();

        // TODO : Target add buff
        //target.OnTargetedBySpell(this);

        //EventManager.OnSpellCasted();
    }
}