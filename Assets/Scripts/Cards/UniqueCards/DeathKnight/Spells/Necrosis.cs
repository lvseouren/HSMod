public class Necrosis : SpellCard
{
    public Necrosis()
    {
        Name = "Necrosis";
        Description = "Deal 4 damage. Costs (1) less for each minion that died this turn.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Rare;

        TargetType = TargetType.TargetAll;

        BaseCost = 4;
    }

    public override void Cast(ICharacter target)
    {
        //EventManager.OnSpellPreCast();

        target.Damage(4 + this.Player.SpellPower);

        //EventManager.OnSpellCasted();
    }
}