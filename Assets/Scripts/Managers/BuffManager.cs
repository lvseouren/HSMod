using System.Collections.Generic;
using System.Reactive.Subjects;

public class BuffManager
{
    public Subject<MinionCard> OnPlayed = new Subject<MinionCard>();

    public List<BaseBuff> AllBuffs;

    public void Add(BaseBuff buff)
    {
        
    }
    
    public void OnTurnStart()
    {
        foreach (BaseBuff buff in AllBuffs)
        {
            buff.OnTurnStart();
        }
    }
    
    public void OnTurnEnd()
    {
        foreach (BaseBuff buff in AllBuffs)
        {
            buff.OnTurnEnd();
        }
    }

    public void RemoveAll()
    {
        
    }
}