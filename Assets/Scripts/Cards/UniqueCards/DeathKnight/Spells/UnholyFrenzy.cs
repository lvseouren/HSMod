public class UnholyFrenzy : SpellCard
{
    public UnholyFrenzy()
    {
        Name = "Unholy Frenzy";
        Description = "Deal 1 damage to a minion. Give it +4 Attack";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        Cost = 1;
    }

    public override void Cast(ICharacter target)
    {
        //EventManager.OnSpellPreCast();

        target.Damage(2 + this.Player.SpellDamage);

        //EventManager.OnSpellCasted();
    }
}
