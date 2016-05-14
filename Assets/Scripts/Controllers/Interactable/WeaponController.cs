using UnityEngine;

public class WeaponController : BaseController
{
    public WeaponCard Weapon;

    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer TokenRenderer;

    public TextMesh AttackText;
    public TextMesh DurabilityText;

    public static WeaponController Create(WeaponCard weapon, Vector3 weaponPosition)
    {
        GameObject heroObject = new GameObject("Weapon_" + weapon.Name);
        heroObject.transform.position = weaponPosition;
        heroObject.transform.localScale = new Vector3(50f, 50f, 50f);
        heroObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        BoxCollider weaponCollider = heroObject.AddComponent<BoxCollider>();
        weaponCollider.size = new Vector3(2f, 2f, 1f);

        WeaponController weaponController = heroObject.AddComponent<WeaponController>();
        weaponController.Weapon = weapon;

        weaponController.Initialize();

        return weaponController;
    }

    // TODO
}