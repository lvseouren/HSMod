public class HeroPreAttackEvent
{
    public Hero Hero;
    public ICharacter Target;
    public int Damage;

    public PreStatus Status = PreStatus.Normal;

    public void SwitchTargetTo(ICharacter other)
    {
        if (Status != PreStatus.Cancelled)
        {
            Target = other;
            Status = PreStatus.TargetSwitched;
        }
    }

    public void Cancel()
    {
        Status = PreStatus.Cancelled;
    }
}