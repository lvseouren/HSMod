public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        TargetType = TargetType.TargetAll;

        BaseCost = 2;
    }

    public override void Cast(ICharacter target)
    {
        SpellPreCastEvent spellPreCastEvent = EventManager.Instance.OnSpellPreCast(this.Player, this);

        if (spellPreCastEvent.IsCancelled == false)
        {
            if (target is Hero)
            {
                Hero heroTarget = (Hero) target;

                HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(null, heroTarget);

                if (heroPreDamageEvent.IsCancelled == false)
                {
                    int damage = 2 + this.Player.GetSpellPower();

                    heroTarget.Damage(damage);

                    EventManager.Instance.OnHeroDamaged(null, heroTarget, damage);
                }
            }
            else if (target is MinionCard)
            {
                MinionCard minionTarget = (MinionCard) target;

                // TODO                

                target.Damage(2 + this.Player.GetSpellPower());
            }

            // TODO : Summon a 1/1 Ghoul with Charge.
        }

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }
}