using UnityEngine;

public class WeaponController : BaseController
{
    public Weapon Weapon;

    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer OpenTokenRenderer;
    public SpriteRenderer ClosedTokenRenderer;

    public NumberController AttackText;
    public NumberController DurabilityText;

    public static WeaponController Create(Player player, Weapon weapon)
    {
        GameObject heroObject = new GameObject("WeaponController");
        heroObject.transform.ChangeParentAt(player.transform, new Vector3(-2f, 0f, 0f));

        BoxCollider weaponCollider = heroObject.AddComponent<BoxCollider>();
        weaponCollider.size = new Vector3(2f, 2f, 1f);

        WeaponController weaponController = heroObject.AddComponent<WeaponController>();
        weaponController.Weapon = weapon;

        weaponController.Initialize();

        return weaponController;
    }


}