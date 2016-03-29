
public class CorpseExplosion : SpellCard
{
    public CorpseExplosion()
    {
        Name = "Corpse Explosion";
        Description = "Give a minion "Deathrattle: Deal 2 damage to all enemy minions.".";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        Cost = 2;
    }

    public override void Cast(ICharacter target)
    {
        //EventManager.OnSpellPreCast();

        target.Damage(2 + this.Player.SpellDamage);

        //EventManager.OnSpellCasted();
    }
}
