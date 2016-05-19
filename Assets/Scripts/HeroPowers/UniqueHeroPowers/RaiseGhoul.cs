public class RaiseGhoul : BaseHeroPower
{
    public RaiseGhoul(Hero hero)
    {
        Name = "Raise Ghoul";
        Description = "Summon a 1/1 Ghoul with Charge that dies at end of turn.";
        Class = HeroClass.DeathKnight;

        TargetType = TargetType.NoTarget;

        BaseCost = 2;

        Hero = hero;

        Initialize();
    }

    public override void Use(Character target)
    {
        MinionCard ghoul = new Ghoul();
        ghoul.SetOwner(Hero.Player);

        Hero.Player.SummonMinion(ghoul);
    }

    public override void Upgrade()
    {
        // TODO
    }
}