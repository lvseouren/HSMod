public class MinionPreDamageEvent
{
    public ICharacter Attacker;
    public MinionCard Minion;

    public bool IsCancelled
    {
        get { return _isCancelled; }
    }

    private bool _isCancelled = false;

    // TODO : Switch target method

    public void Cancel()
    {
        _isCancelled = true;
    }

    // TODO : Attack type? Maybe melee, spell, passive, etc
}