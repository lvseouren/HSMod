public class SpellPreCastEvent
{
    public Player Player;
    public SpellCard Spell;
    public ICharacter Target;

    public PreStatus Status = PreStatus.Normal;

    public void SwitchTargetTo(ICharacter other)
    {
        if (Target != null)
        {
            if (Status != PreStatus.Cancelled)
            {
                Target = other;
                Status = PreStatus.TargetSwitched;
            }
        }
    }

    public void Cancel()
    {
        Status = PreStatus.Cancelled;
    }
}