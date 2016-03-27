public class HeroPreAttackEvent
{
    private bool _willAttack = true;

    public Hero Hero;
    // TODO : Target enemy (hero/minion) as the same type (interface IAttackable ?)

    // TODO : Switch target method

    public void Cancel()
    {
        _willAttack = false;
    }
}