public class SpellPreCastEvent
{
    public Player Player;
    public SpellCard Spell;
    public Character Target;

    public PreStatus Status = PreStatus.Normal;

    public void SwitchTargetTo(Character other)
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