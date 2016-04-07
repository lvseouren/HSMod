public class BaseBuff
{
    public MinionCard Minion;
    public BuffType BuffType;

    public BaseBuff(MinionCard minion)
    {
        Minion = minion;
    }

    public virtual void OnAdded()
    {
        
    }

    public virtual void OnRemoved()
    {

    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnTurnEnd()
    {
        
    }
}

public enum BuffType
{
    Area,
    Buff,
    Debuff,
    Effect
}