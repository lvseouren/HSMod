public class HeroPreDamageEvent
{
    public Hero Hero;
    public ICharacter Attacker;
    public int Damage;

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