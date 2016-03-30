using UnityEngine;

public class MinionCard : BaseCard, ICharacter
{
    // Base Stats //
    public int BaseAttack { get; set; }
    public int BaseHealth { get; set; }
    public MinionType MinionType;

    // In-Game Stats //
    public int CurrentAttack { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }

    // Effects //
    public bool Taunt = false;
    public bool Charge = false;
    public bool Stealth = false;
    public bool DivineShield = false;
    public bool Elusive = false;
    public bool Forgetful = false;
    public bool Frozen = false;
    public bool Silenced = false;
    public int SpellDamage = 0;

    #region Events

    public virtual void OnPlayed()
    {

    }

    public virtual void OnPreAttack()
    {

    }

    public virtual void OnAttack()
    {

    }

    public virtual void OnPreDamaged(ICharacter attacker, ref int damageAmount)
    {
        
    }

    public virtual void OnDamaged(ICharacter attacker, int damageAmount)
    {
        
    }

    public virtual void OnDead()
    {

    }

    public virtual void OnSelectedBySpell()
    {

    }

    public virtual void OnPassiveAbilityUsed()
    {

    }

    public virtual void OnTurnEnd()
    {
        
    }

    #endregion

    #region Methods

    public void AddBuff(object buff)
    {
        // TODO : Buff list + buff class probably
    }

    public void Attack(ICharacter target)
    {
        //EventManager.OnMinionPreAttack(this, target);

        // TODO : Check for enemy count > 0
        if (this.Forgetful){

            if (Random.Range(0, 1) == 1)
            {
                // target = Random target
            }
        }

        if (target is Hero)
        {
            //EventManager.OnHeroPreDamaged(this, target);

            // TODO : Attack stuff

            //EventManager.OnHeroDamaged(this, target, damageAmount);
        }
        else if (target is MinionCard)
        {
            //EventManager.OnMinionPreDamaged(this, target);

            // TODO : Attack and receive damage too

            //EventManager.OnMinionDamaged(this, target, damageAmount);
        }

        //EventManager.OnMinionAttack(this, target, damage);
    }

    public void Damage(ICharacter character, int damageAmount)
    {
        // TODO : NEEDS EVENT CLASSES TO MANAGE DAMAGE INFO AROUND THE METHOD

        this.OnPreDamaged(character, ref damageAmount);

        // TODO : Sprite -> Show health loss on card
        this.BaseHealth -= damageAmount;

        this.OnDamaged(character, damageAmount);

        if (this.BaseHealth < 0)
        {
            Die();
        }
    }

    public void Spawn()
    {
        // TODO : Custom animations, sounds, etc ?
        // TODO : Position in battlefield
        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
    }

    public void Die()
    {
        //EventManager.OnMinionDied(this);
        // TODO : Custom animations, sounds, etc ?
        // TODO : Add card to list of dead minions
        Destroy();
    }

    public void Destroy()
    {
        // TODO : Play destroy animation (dust and stuff)
        // TODO : Remove card from battlefield
    }

    public void Transform(MinionCard other)
    {
        // TODO : Play transform animation
        // TODO : Transform minion without triggering anything
    }

    public bool IsAlive()
    {
        return this.CurrentHealth > 0;
    }

    #endregion
}

public enum MinionType
{
    // Classic Types //
    General,
    Murloc,
    Beast,
    Mech,
    Dragon,
    Pirate,
    Demon,
    Totem,

    // Custom Types //
    Undead
}