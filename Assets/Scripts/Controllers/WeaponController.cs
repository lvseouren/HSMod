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
        WeaponRenderer = CreateRenderer("Weapon", Vector3.one, Vector3.zero, 0);
        TokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 1);

        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, 2);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, 3);
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

    private SpriteRenderer CreateRenderer(string name, Vector3 scale, Vector3 position, int order)
    {
        // Creating a GameObject to hold the SpriteRenderer
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = this.transform;
        glowObject.transform.localEulerAngles = Vector3.zero;
        glowObject.transform.localPosition = position;
        glowObject.transform.localScale = scale;

        // Creating the SpriteRenderer and adding it to the GameObject
        SpriteRenderer glowRenderer = glowObject.AddComponent<SpriteRenderer>();
        glowRenderer.sortingLayerName = "Game";
        glowRenderer.sortingOrder = order;
        glowRenderer.enabled = false;

        return glowRenderer;
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