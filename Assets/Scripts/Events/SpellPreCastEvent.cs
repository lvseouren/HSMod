public class SpellPreCastEvent
{
    public Player Player;
    public SpellCard Spell;

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
}