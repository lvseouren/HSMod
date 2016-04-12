public class Necrosis : SpellCard
{
    public Necrosis()
    {
        Name = "Necrosis";
        Description = "Deal 4 damage. Costs (1) less for each minion that died this turn.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Rare;

        TargetType = TargetType.AllCharacters;

        BaseCost = 4;
    }

    // TODO : Cost reduction

    public override void Cast(ICharacter target)
    {
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this.Player, this);

        if (spellPreCastEvent.IsCancelled == false)
        {
            target.Damage(4 + this.Player.GetSpellPower());
        }

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }
}