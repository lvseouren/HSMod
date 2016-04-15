public class RaiseGhoul : BaseHeroPower
{
    public RaiseGhoul(Hero hero)
    {
        Name = "Raise Ghoul";
        Description = "Summon a 1/1 Ghoul with Charge that dies at end of turn.";
        Class = HeroClass.DeathKnight;

        Hero = hero;
    }

    public override void Use()
    {
        // TODO : Summon 1/1 Ghoul
    }
}