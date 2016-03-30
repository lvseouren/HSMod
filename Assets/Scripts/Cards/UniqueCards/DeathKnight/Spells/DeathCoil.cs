public class DeathCoil : SpellCard
{
    public DeathCoil()
    {
        Name = "Death Coil";
        Description = "Deal 2 damage to a minion. If it's a friendly Undead, restore it to full Health instead.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        TargetType = TargetType.TargetAllMinions;

        BaseCost = 1;
    }

    public override void Cast(MinionCard target)
    {
        //EventManager.OnSpellPreCast();

        if (target.MinionType == MinionType.Undead && target.Player == this.Player)
        {
            // TODO : Heal animation and sound
            target.CurrentHealth = target.MaxHealth;
            
            target.OnSelectedBySpell();
        }
        else
        {
            bool isCancelled = EventManager.Instance.OnMinionPreDamaged(this.Player.Hero, target);

            if (isCancelled == false)
            {
                target.Damage(null, 2 + this.Player.SpellDamage);
            }
        }

        //EventManager.OnSpellCasted();
    }
}
