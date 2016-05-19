public abstract class Character
{
    #region Base Stats

    public int BaseHealth = 0;
    public int BaseAttack = 0;
    public int BaseArmor = 0;

    #endregion

    #region In-Game Stats

    public Player Player;
    public BaseController Controller;

    public int MaxHealth;
    public int CurrentHealth;
    public int CurrentAttack;
    public int CurrentArmor;

    public bool IsSleeping = true;
    public int CurrentTurnAttacks = 0;

    #endregion

    #region In-Game Effects

    public bool HasTaunt = false;
    public bool HasCharge = false;
    public bool HasPoison = false;
    public bool HasWindfury = false;
    public bool HasDivineShield = false;
    public bool IsImmune = false;
    public bool IsElusive = false;
    public bool IsForgetful = false;
    public bool IsStealth = false;

    public int SpellPower = 0;

    public bool IsSilenced = false;
    public bool IsFrozen = false;
	public bool IsEnraged = false;
    public bool UnfreezeNextTurn = false;

    #endregion

    #region Methods

    public virtual void Initialize()
    {
        MaxHealth = BaseHealth;
        CurrentHealth = BaseHealth;
        CurrentAttack = BaseAttack;
        CurrentArmor = BaseArmor;

        IsSleeping = !HasCharge;
    }

    public virtual void Attack(Character target) { }

    public virtual void Damage(Character attacker, int damage) { }

    public virtual void Heal(int heal) { }

    public virtual void CheckDeath() { }

    #endregion

    #region Getter Methods
    
    public virtual int GetMissingHealth()
    {
        return MaxHealth - CurrentHealth;
    }

    #endregion

    #region Condition Checkers

    public virtual bool CanAttack()
    {
        return false;
    }

    public virtual bool CanAttackTo(Character target)
    {
        if (target.IsHero())
        {
            if (target.As<Hero>().Player.HasTauntMinions() == false)
            {
                return true;
            }
            return false;
        }
        else
        {
            Minion targetMinion = target.As<Minion>();

            if (targetMinion.IsStealth) return false;

            if (targetMinion.HasTaunt)
            {
                return true;
            }
            else
            {
                if (targetMinion.Player.HasTauntMinions())
                {
                    return false;
                }
                return true;
            }
        }
    }

    public virtual bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    public virtual bool IsHero()
    {
        return false;
    }

    public virtual bool IsMinion()
    {
        return false;
    }

    public virtual bool IsFriendlyOf(Character other)
    {
        if (this.IsHero())
        {
            if (other.IsHero())
            {
                return this == other;
            }
            else
            {
                return this.Player.Minions.Contains(other.As<Minion>());
            }
        }
        else
        {
            if (other.IsHero())
            {
                return other.Player.Minions.Contains(this.As<Minion>());
            }
            else
            {
                return this.Player.Minions.Contains(other.As<Minion>());
            }
        }
    }

    public virtual bool IsEnemyOf(Character other)
    {
        return (this.IsFriendlyOf(other) == false);
    }

    public bool IsDamaged()
    {
        return CurrentHealth != MaxHealth;
    }

    #endregion
}