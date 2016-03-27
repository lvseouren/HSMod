public class MinionPreAttackEvent
{
    private bool _isCancelled = false;

    public MinionCard Minion;
    public IDamageable Target;
    public bool IsCancelled
    {
        get { return _isCancelled; }
    }

    // TODO : Switch target method

    public void Cancel()
    {
        _isCancelled = true;
    }
}