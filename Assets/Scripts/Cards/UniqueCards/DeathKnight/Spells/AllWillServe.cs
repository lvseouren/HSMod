public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

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