public class MinionCard : BaseCard
{
    #region Base Stats

    public int BaseAttack;
    public int BaseHealth;
    public MinionType MinionType;

    #endregion

    #region In-Game Stats

    public Minion Minion;

    public int MaxHealth;
    public int CurrentHealth;
    public int CurrentAttack;

    #endregion

    #region In-Game Effects

    public bool HasTaunt = false;
    public bool HasCharge = false;
    public bool HasPoison = false;
    public bool HasWindfury = false;
    public bool HasDivineShield = false;
    public bool IsImmune = false;
    public bool IsElusive = false;
    public bool IsStealth = false;

    public int SpellPower = 0;

    public bool IsSilenced = false;
    public bool IsFrozen = false;
    public bool IsForgetful = false;

    #endregion

    public BuffManager Buffs = new BuffManager();

    // TODO : Rewrite buffs for cards
    
    public MinionCard()
    {
        CurrentAttack = BaseAttack;

        CurrentHealth = BaseHealth;
        MaxHealth = BaseHealth;
    }

    public void AddBuff(BaseBuff buff)
    {
        // Adding the buff to the list
        Buffs.AllBuffs.Add(buff);

        // Firing OnAdded for that buff
        buff.OnAdded(this.Minion);
    }

    public void RemoveBuff(BaseBuff buff)
    {
        // Checking if the minion has the buff
        if (Buffs.AllBuffs.Contains(buff))
        {
            // Removing the buff from the list
            Buffs.AllBuffs.Remove(buff);

            // Firing OnRemoved for that buff
            buff.OnRemoved(this.Minion);
        }
    }
}