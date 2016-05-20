public class Weapon
{
    public Player Player;

    public WeaponCard Card;

    public WeaponController Controller;

    public int BaseAttack;
    public int CurrentAttack;

    public int BaseDurability;
    public int CurrentDurability;

    public bool HasWindfury;

    #region Constructor

    public Weapon(WeaponCard card)
    {
        Card = card;

        CurrentAttack = card.CurrentAttack;
        BaseAttack = card.BaseAttack;

        CurrentDurability = card.CurrentDurability;
        BaseDurability = card.BaseDurability;

        HasWindfury = Card.HasWindfury;
    }

    #endregion

    #region Methods

    public virtual void Use()
    {
        Debugger.LogWeapon(this, "used");

        CurrentDurability--;

        if (CurrentDurability <= 0)
        {
            Remove();
        }
    }

    #endregion
}