public class DeathCoil : SpellCard
{
    public DeathCoil()
    {
        Name = "Death Coil";
        Description = "Deal 2 damage to a minion. If it's a friendly Undead, restore it to full Health instead.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        TargetType = TargetType.AllMinions;

        BaseCost = 1;
    }

    public override void Cast(ICharacter target)
    {
        if (target.IsFriendlyOf(this.Player.Hero))
        {
            target.Heal(target.GetMissingHealth());
        }
        else
        {
            int damage = 2 + this.Player.GetSpellPower();

            target.TryDamage(null, damage);
        }
    }
}