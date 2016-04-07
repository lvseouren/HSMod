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
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this.Player, this);

        if (spellPreCastEvent.IsCancelled == false)
        {
            if (target.MinionType == MinionType.Undead && target.Player == this.Player)
            {
                // TODO : Heal animation and sound
                target.CurrentHealth = target.MaxHealth;

                target.BuffManager.OnTargetedBySpell.OnNext(this);
            }
            else
            {
                MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(this.Player.Hero, target);

                if (minionPreDamageEvent.IsCancelled == false)
                {
                    target.Damage(2 + this.Player.GetSpellPower());

                    target.BuffManager.OnTargetedBySpell.OnNext(this);

                    target.CheckDie();
                }
            }
        }     

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }
}
