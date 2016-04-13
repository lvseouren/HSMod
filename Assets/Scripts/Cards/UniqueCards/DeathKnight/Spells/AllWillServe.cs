public class AllWillServe : SpellCard
{
    public AllWillServe()
    {
        Name = "All Will Serve";
        Description = "Deal 2 damage. Summon a 1/1 Ghoul with Charge.";

        Class = HeroClass.DeathKnight;
        Rarity = CardRarity.Common;

        BaseCost = 2;
    }

    // TODO : Move this to base class somehow
    public override void Cast(ICharacter target)
    {
        int damage = 2 + this.Player.GetSpellPower();

        if (target is Hero)
        {
            Hero heroTarget = (Hero) target;

            HeroPreDamageEvent heroPreDamageEvent = EventManager.Instance.OnHeroPreDamage(null, heroTarget);

            if (heroPreDamageEvent.IsCancelled == false)
            {
                heroTarget.Damage(damage);

                EventManager.Instance.OnHeroDamaged(null, heroTarget, damage);
            }
        }
        else if (target is MinionCard)
        {
            MinionCard minionTarget = (MinionCard) target;

            MinionPreDamageEvent minionPreDamageEvent = EventManager.Instance.OnMinionPreDamage(null, minionTarget);

            if (minionPreDamageEvent.IsCancelled == false)
            {
                minionTarget.BuffManager.OnPreDamage.OnNext(damage);

                minionTarget.Damage(damage);

                minionTarget.BuffManager.OnDamaged.OnNext(null);

                EventManager.Instance.OnMinionDamaged(null, minionTarget);
            }
        }

        // TODO : Summon 1/1
    }
}