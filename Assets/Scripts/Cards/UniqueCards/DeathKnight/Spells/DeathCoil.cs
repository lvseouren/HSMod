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

        InitializeSpell();
    }

    public override void Cast(Character target)
    {
        if (target.IsFriendlyOf(Player.Hero))
        {
            target.Heal(target.GetMissingHealth());
        }
        else
        {
            int damage = 2 + Player.GetSpellPower();

            target.Damage(null, damage);
        }
    }
}