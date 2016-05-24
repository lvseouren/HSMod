using UnityEngine;

public class WeaponController : BaseController
{
    public Weapon Weapon;

    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer OpenTokenRenderer;
    public SpriteRenderer ClosedTokenRenderer;

    public NumberController AttackController;
    public NumberController DurabilityController;

    public static WeaponController Create(Player player, Weapon weapon)
    {
        GameObject heroObject = new GameObject("WeaponController");
        heroObject.transform.ChangeParentAt(player.transform, new Vector3(-4f, 0.5f, 0f));
        heroObject.transform.localScale = Vector3.one * 1.3f;

        BoxCollider weaponCollider = heroObject.AddComponent<BoxCollider>();
        weaponCollider.size = new Vector3(2f, 2f, 1f);

        WeaponController weaponController = heroObject.AddComponent<WeaponController>();
        weaponController.Weapon = weapon;

        weaponController.Initialize();

        return weaponController;
    }

    public override void Initialize()
    {
        // Creating the Attack and Health NumberControllers
        AttackController = NumberController.Create("Attack_Controller", this.gameObject, new Vector3(-0.8f, -0.95f, 0f), 15, 0.35f);
        DurabilityController = NumberController.Create("Durability_Controller", this.gameObject, new Vector3(0.825f, -0.95f, 0f), 15, 0.35f);

        // Creating the SpriteRenderers for the token, the minion and its glows
        OpenTokenRenderer = CreateRenderer("OpenToken_Sprite", Vector3.one, Vector3.zero, 14);
        ClosedTokenRenderer = CreateRenderer("ClosedToken_Sprite", Vector3.one, Vector3.zero, 14);
        WeaponRenderer = CreateRenderer("Weapon_Sprite", Vector3.one, Vector3.zero, 13);
        WhiteGlowRenderer = CreateRenderer("WhiteGlow_Sprite", Vector3.one * 2f, Vector3.zero, 12);
        GreenGlowRenderer = CreateRenderer("GreenGlow_Sprite", Vector3.one * 2f, Vector3.zero, 11);
        RedGlowRenderer = CreateRenderer("RedGlow_Sprite", Vector3.one * 2f, Vector3.zero, 10);

        // Initializing the SpriteRenderers and the NumberControllers
        UpdateSprites();
        UpdateNumbers();

        // Enabling the token and the mininon
        OpenTokenRenderer.enabled = true;
        WeaponRenderer.enabled = true;

        // Enabling both NumberControllers
        AttackController.SetEnabled(true);
        DurabilityController.SetEnabled(true);
    }

    public override void DestroyController()
    {
        // Removing the NumberControllers
        AttackController.Remove();
        DurabilityController.Remove();

        // Destroying the SpriteRenderers
        Destroy(OpenTokenRenderer);
        Destroy(ClosedTokenRenderer);
        Destroy(WeaponRenderer);
        Destroy(WhiteGlowRenderer);
        Destroy(GreenGlowRenderer);
        Destroy(RedGlowRenderer);

        // Destroying the main GameObject
        Destroy(this.gameObject);
    }

    public override void UpdateSprites()
    {
        // Loading the sprites in the SpriteRenderers
        OpenTokenRenderer.sprite = SpriteManager.Instance.Tokens["Weapon_Open"];
        ClosedTokenRenderer.sprite = SpriteManager.Instance.Tokens["Weapon_Closed"];
        WeaponRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Weapon.Card.Class.Name() + "/Weapons/" + Weapon.Card.TypeName());
        WhiteGlowRenderer.sprite = SpriteManager.Instance.Glows["Weapon_WhiteGlow"];
        GreenGlowRenderer.sprite = SpriteManager.Instance.Glows["Weapon_GreenGlow"];
        RedGlowRenderer.sprite = SpriteManager.Instance.Glows["Weapon_RedGlow"];

        // Updating the green glow status depending on the Minion status
        GreenGlowRenderer.enabled = Weapon.Player.Hero.CanAttack();
    }
    
    public override void UpdateNumbers()
    {
        if (Weapon.CurrentAttack < Weapon.BaseAttack)
        {
            AttackController.UpdateNumber(Weapon.CurrentAttack, "Red");
        }
        else if (Weapon.CurrentAttack == Weapon.BaseAttack)
        {
            AttackController.UpdateNumber(Weapon.CurrentAttack, "White");
        }
        else
        {
            AttackController.UpdateNumber(Weapon.CurrentAttack, "Green");
        }

        if (Weapon.CurrentDurability < Weapon.BaseDurability)
        {
            DurabilityController.UpdateNumber(Weapon.CurrentDurability, "Red");
        }
        else if (Weapon.CurrentDurability == Weapon.BaseDurability)
        {
            DurabilityController.UpdateNumber(Weapon.CurrentDurability, "White");
        }
        else
        {
            DurabilityController.UpdateNumber(Weapon.CurrentDurability, "Green");
        }
    }
}