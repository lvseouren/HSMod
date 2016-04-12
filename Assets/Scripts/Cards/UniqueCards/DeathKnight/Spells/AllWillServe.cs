public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

        CardClass = CardClass.DeathKnight;
        Rarity = Rarity.Common;

        TargetType = TargetType.AllCharacters;

        BaseCost = 2;
    }

    // TODO : Move this to base class somehow
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

                minionTarget.BuffManager.OnPreDamage.OnNext(null);

                MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(null, minionTarget);

                if (minionPreDamageEvent.IsCancelled == false)
                {
                    int damage = 2 + this.Player.GetSpellPower();

                    minionTarget.Damage(damage);

                    minionTarget.BuffManager.OnDamaged.OnNext(null);

                    EventManager.Instance.OnMinionDamaged(null, minionTarget);
                }
            }

            // TODO : Summon a 1/1 Ghoul with Charge.
        }

        EventManager.Instance.OnSpellCasted(this.Player, this);
    }
}