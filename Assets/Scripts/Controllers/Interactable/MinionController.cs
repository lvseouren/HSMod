using UnityEngine;

public class MinionController : BaseController
{
    public Minion Minion;

    public Vector3 TargetPosition;

    private SpriteRenderer TokenRenderer;
    private SpriteRenderer MinionRenderer;

    private NumberController AttackController;
    private NumberController HealthController;

    private BoxCollider Collider;

    // TODO : Frozen, Silenced, DivineShield, Taunt, etc... renderers/overlays

    public static MinionController Create(BoardController parentBoard, Minion minion)
    {
        // Creating a new GameObject to hold all the components
        GameObject minionObject = new GameObject(minion.Card.Name);
        minionObject.transform.ChangeParent(parentBoard.transform);

        // Adding a BoxCollider to the GameObject
        BoxCollider collider = minionObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(2.5f, 3.5f, 0.5f);

        // Adding a MinionController to the GameObject
        MinionController minionController = minionObject.AddComponent<MinionController>();
        minionController.Minion = minion;
        minionController.Collider = collider;

        // Initializing the MinionController
        minionController.Initialize();

        return minionController;
    }

    public override void Initialize()
    {
        // Creating the Attack and Health NumberControllers
        AttackController = NumberController.Create("Attack_Controller", this.gameObject, new Vector3(-0.8f, -0.95f, 0f), 15, 0.35f);
        HealthController = NumberController.Create("Health_Controller", this.gameObject, new Vector3(0.825f, -0.95f, 0f), 15, 0.35f);

        // Creating the SpriteRenderers for the token, the minion and its glows
        TokenRenderer = CreateRenderer("Token_Sprite", Vector3.one, Vector3.zero, 14);
        MinionRenderer = CreateRenderer("Minion_Sprite", Vector3.one, Vector3.zero, 13);
        WhiteGlowRenderer = CreateRenderer("WhiteGlow_Sprite", Vector3.one * 2f, Vector3.zero, 12);
        GreenGlowRenderer = CreateRenderer("GreenGlow_Sprite", Vector3.one * 2f, Vector3.zero, 11);
        RedGlowRenderer = CreateRenderer("RedGlow_Sprite", Vector3.one * 2f, Vector3.zero, 10);
        
        // Initializing the SpriteRenderers and the NumberControllers
        UpdateSprites();
        UpdateNumbers();

        // Enabling the token and the mininon
        TokenRenderer.enabled = true;
        MinionRenderer.enabled = true;

        // Enabling both NumberControllers
        AttackController.SetEnabled(true);
        HealthController.SetEnabled(true);
    }

    public override void DestroyController()
    {
        // Removing the NumberControllers
        AttackController.Remove();
        HealthController.Remove();

        // Destroying the SpriteRenderers
        Destroy(TokenRenderer);
        Destroy(MinionRenderer);
        Destroy(WhiteGlowRenderer);
        Destroy(GreenGlowRenderer);
        Destroy(RedGlowRenderer);

        // Destroying the main GameObject
        Destroy(this.gameObject);
    }

    public override void UpdateSprites()
    {
        // Getting the path strings
        string tokenPath = GetTokenPath();
        string glowPath = GetGlowPath();

        // Loading the sprites in the SpriteRenderers
        TokenRenderer.sprite = SpriteManager.Instance.Tokens[tokenPath];
        MinionRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Minion.Card.Class.Name() + "/Minions/" + Minion.Card.TypeName());
        WhiteGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "WhiteGlow"];
        GreenGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "GreenGlow"];
        RedGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "RedGlow"];
    }

    // TODO : Rewrite
    public override void UpdateNumbers()
    {
        if (Minion.CurrentAttack < Minion.BaseAttack)
        {
            AttackController.UpdateNumber(Minion.CurrentAttack, "Red");
        }
        else if (Minion.CurrentAttack == Minion.BaseAttack)
        {
            AttackController.UpdateNumber(Minion.CurrentAttack, "White");
        }
        else
        {
            AttackController.UpdateNumber(Minion.CurrentAttack, "Green");
        }

        if (Minion.CurrentHealth < Minion.BaseHealth)
        {
            HealthController.UpdateNumber(Minion.CurrentHealth, "Red");
        }
        else if (Minion.CurrentHealth == Minion.BaseHealth)
        {
            HealthController.UpdateNumber(Minion.CurrentHealth, "White");
        }
        else
        {
            HealthController.UpdateNumber(Minion.CurrentHealth, "Green");
        }
    }

    private string GetTokenPath()
    {
        if (Minion.Card.Rarity == CardRarity.Legendary)
        {
            return "Minion_Legendary";
        }

        return "Minion_Normal";
    }

    private string GetGlowPath()
    {
        string glowString = "Minion_";

        if (Minion.Card.Rarity == CardRarity.Legendary)
        {
            glowString += "Legendary";
        }
        else
        {
            glowString += "Normal";
        }
        
        if (Minion.HasTaunt)
        {
            glowString += "Taunt";
        }

        return glowString + "_";

        // Probably will look into something like a semi-transparent overlay instead of new sprites for Frozen and Stealth

        if (Minion.IsFrozen)
        {
            glowString += "Frozen_";
        }

        if (Minion.IsStealth)
        {
            glowString += "Stealth_";
        }

        return glowString;
    }

    #region Unity Messages

    private void Update()
    {
        transform.localPosition = TargetPosition;
    }

    private void OnMouseEnter()
    {
        SetWhiteRenderer(true);

        InterfaceManager.Instance.OnHoverStart(this);
    }

    private void OnMouseExit()
    {
        SetWhiteRenderer(false);

        InterfaceManager.Instance.OnHoverStop();
    }

    private void OnMouseDown()
    {
        if (Minion.CanAttack())
        {
            InterfaceManager.Instance.EnableArrow(this);
        }
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();

        if (Minion.Player == GameManager.Instance.CurrentPlayer && Minion.CanAttack())
        {
            Character target = Util.GetCharacterAtMouse();

            if (target != null)
            {
                if (Minion.CanAttack() && Minion.CanAttackTo(target))
                {
                    // TODO : Animations, sounds, etc...
                    Minion.Attack(target);
                }
            }
        }
    }

    #endregion
}