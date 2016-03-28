public class DeathCoil : SpellCard
{
    public DeathCoil()
    {
        Name = "Death Coil";
        Description = "Deal 2 damage to a minion. If it's a friendly Undead, restore it to full Health instead.";

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
