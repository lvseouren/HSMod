using UnityEngine;

public class WeaponController : BaseController
{
    public WeaponCard Weapon;

    public bool CanTarget = true;

    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer TokenRenderer;

    public static void Create(WeaponCard weapon)
    {
        GameObject weaponObject = new GameObject(weapon.Player.name + "_" + weapon.Name);

        WeaponController weaponController = weaponObject.AddComponent<WeaponController>();
        weaponController.Weapon = weapon;

        weaponController.Initialize();
    }

    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, -2);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -1);

        WeaponRenderer = CreateRenderer("Weapon", Vector3.one, Vector3.zero, 0);

        TokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 1);
    }

    public override void Remove()
    {
        WeaponRenderer.DisposeSprite();
        Destroy(WeaponRenderer);

        TokenRenderer.DisposeSprite();
        Destroy(TokenRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        TokenRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Loading the sprites
        TokenRenderer.sprite = Resources.Load<Sprite>("Sprites/General/WeaponToken");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Weapon_GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Weapon_RedGlow");
    }
    
    #region Unity Messages
    
    private void OnMouseDown()
    {
        if (this.CanTarget)
        {
            InterfaceManager.Instance.EnableArrow();
        }
    }

    private void OnMouseUp()
    {
        // TODO : Check target

        InterfaceManager.Instance.DisableArrow();
    }

    #endregion
}