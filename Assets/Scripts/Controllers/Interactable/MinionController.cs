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

    // TODO : Frozen, Silenced, DivineShield, Taunt, etc... renderers

    public static MinionController Create(BoardController parentBoard, Minion minion)
    {
        GameObject minionObject = new GameObject(minion.Card.Name);
        minionObject.transform.ChangeParent(parentBoard.transform);

        BoxCollider collider = minionObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(2.5f, 3.5f, 0.5f);

        MinionController minionController = minionObject.AddComponent<MinionController>();
        minionController.Minion = minion;
        minionController.Collider = collider;

        minionController.Initialize();

        return minionController;
    }

    public override void Initialize()
    {
        AttackController = NumberController.Create("Attack_Controller", this.gameObject, new Vector3(-0.8f, -0.95f, 0f), 15, 0.35f);
        HealthController = NumberController.Create("Health_Controller", this.gameObject, new Vector3(0.825f, -0.95f, 0f), 15, 0.35f);

        TokenRenderer = CreateRenderer("Token_Sprite", Vector3.one, Vector3.zero, 14);

        MinionRenderer = CreateRenderer("Minion_Sprite", Vector3.one, Vector3.zero, 13);

        WhiteGlowRenderer = CreateRenderer("WhiteGlow_Sprite", Vector3.one * 2f, Vector3.zero, 12);
        GreenGlowRenderer = CreateRenderer("GreenGlow_Sprite", Vector3.one * 2f, Vector3.zero, 11);
        RedGlowRenderer = CreateRenderer("RedGlow_Sprite", Vector3.one * 2f, Vector3.zero, 10);

        TokenRenderer.enabled = true;
        MinionRenderer.enabled = true;
        
        UpdateSprites();
        UpdateNumbers();

        AttackController.SetEnabled(true);
        HealthController.SetEnabled(true);
    }

    public override void Remove()
    {
        Destroy(TokenRenderer);
        Destroy(MinionRenderer);
        Destroy(WhiteGlowRenderer);
        Destroy(GreenGlowRenderer);
        Destroy(RedGlowRenderer);

        Destroy(this.gameObject);
    }

    public override void UpdateSprites()
    {
        // Getting the path strings
        string tokenPath = GetTokenPath();
        string glowPath = GetGlowPath();

        // Loading the sprites
        TokenRenderer.sprite = SpriteManager.Instance.Tokens[tokenPath];
        
        MinionRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Minion.Card.Class.Name() + "/Minions/" + Minion.Card.TypeName());

        WhiteGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "WhiteGlow"];
        GreenGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "GreenGlow"];
        RedGlowRenderer.sprite = SpriteManager.Instance.Glows[glowPath + "RedGlow"];
    }

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
        if (Minion.IsFrozen)
        {
            Debug.Log("FROZEN MINION CANT ATTACK");
            return;
        }

        if (Minion.CanAttack())
        {
            InterfaceManager.Instance.EnableArrow(this);
        }
    }

    private void OnMouseUp()
    {
        Character target = Util.GetCharacterAtMouse();

        if (target != null)
        {
            if (Minion.CanAttackTo(target))
            {
                // TODO : Animations, etc...
                Minion.Attack(target);
            }
        }

        InterfaceManager.Instance.DisableArrow();
    }

    #endregion
}