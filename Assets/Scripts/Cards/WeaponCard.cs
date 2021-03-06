﻿public class WeaponCard : BaseCard
{
    // Base Stats //
    public int BaseAttack;
    public int BaseDurability;

    // In-Game Stats //
    public int CurrentAttack;
    public int CurrentDurability;

    // Effects //
    public bool HasWindfury = false;
    public bool IsForgetful = false;

    #region Events

    public virtual void Battlecry()
    {

    }

    public virtual void Deathrattle()
    {

    }

    public virtual void Inspire()
    {
        
    }

    public virtual void OnPreAttack()
    {

    }

    public virtual void OnAttacked()
    {

    }

    public virtual void OnMinionPlayed()
    {

    }

    public virtual void OnMinionSummoned()
    {

    }

    public virtual void OnSecretPlayed()
    {

    }

    public virtual void OnSecretRevealed()
    {
        
    }

    public virtual void OnHeroPreDamage()
    {

    }

    public virtual void OnHeroDamaged()
    {

    }

    #endregion

    public void InitializeWeapon()
    {
        InitializeCard();

        CurrentAttack = BaseAttack;
        CurrentDurability = BaseDurability;
    }

    public virtual void Attack()
    {
        CurrentDurability -= 1;
    }

    public override void Play()
    {
        Player.UseMana(CurrentCost);

        Player.EquipWeapon(this);
        Player.RemoveCardFromHand(this);
    }
}