public abstract class BaseBuff
{
    public string Name;
    public string Description;

    public BuffType BuffType;

    public virtual void OnAdded(Minion minion) { }

    public virtual void OnRemoved(Minion minion) { }

    public virtual void OnTurnStart() { }

    public virtual void OnTurnEnd() { }
}