public class RaiseDead : SpellCard
{
    public RaiseDead()
    {
        Name = "Raise Dead";
        Description = "Summon a 3/3 Ghoul. Costs (1) less for each minion that died this turn.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        Cost = 3;
    }

    public override void Cast(ICharacter target)
    {
        //EventManager.OnSpellPreCast();

        target.Damage(2 + this.Player.SpellDamage);

        //EventManager.OnSpellCasted();
    }
}
