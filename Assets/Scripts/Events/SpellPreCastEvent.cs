public class SpellPreCastEvent
{
    public Hero Hero;
    public SpellCard Spell;
    public ICharacter Target;

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