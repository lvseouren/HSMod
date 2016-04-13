public class MinionPreAttackEvent
{
    public MinionCard Minion;
    public ICharacter Target;
    
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