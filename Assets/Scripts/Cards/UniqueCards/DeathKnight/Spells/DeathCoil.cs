public class DeathCoil : SpellCard
{
    public DeathCoil()
    {
        Name = "Death Coil";
        Description = "Deal 2 damage to a minion. If it's a friendly Undead, restore it to full Health instead.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        TargetType = TargetType.AllMinions;

        BaseCost = 1;
    }

    public override void Cast(ICharacter target)
    {
        MinionCard minionTarget = target.As<MinionCard>();

        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this.Player, this);

        if (spellPreCastEvent.IsCancelled == false)
        {
            if (minionTarget.MinionType == MinionType.Undead && minionTarget.Player == this.Player)
            {
                // TODO : Heal animation and sound
                minionTarget.CurrentHealth = minionTarget.MaxHealth;

                minionTarget.BuffManager.OnTargetedBySpell.OnNext(this);
            }
            else
            {
                MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(this.Player.Hero, minionTarget);

                if (minionPreDamageEvent.IsCancelled == false)
                {
                    minionTarget.Damage(2 + this.Player.GetSpellPower());

                    minionTarget.BuffManager.OnTargetedBySpell.OnNext(this);

                    minionTarget.CheckDeath();
                }
            }
        }     

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }
}
