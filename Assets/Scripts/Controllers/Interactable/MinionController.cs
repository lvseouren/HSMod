using UnityEngine;

public class MinionController : BaseController
{
    public Minion Minion;

    public SpriteRenderer TokenRenderer;
    public SpriteRenderer MinionRenderer;

    // TODO : Frozen, Silenced, DivineShield, Taunt, etc... renderers

    public static MinionController Create(BoardController parentBoard, MinionCard minion)
    {
        GameObject minionObject = new GameObject(minion.Name);
        minionObject.transform.ChangeParent(parentBoard.transform);

        MinionController minionController = minionObject.AddComponent<MinionController>();

        minionController.Initialize();

        return minionController;
    }

    public override void Initialize()
    {
        TokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 14);

        MinionRenderer = CreateRenderer("Minion", Vector3.one, Vector3.zero, 13);

        WhiteGlowRenderer = CreateRenderer("WhiteGlow", Vector3.one * 2f, Vector3.zero, 12);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, 11);
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, 10);
        
        UpdateSprites();
        UpdateNumbers();
    }

    public override void Remove()
    {
        Destroy(TokenRenderer);

        MinionRenderer.DisposeSprite();
        Destroy(MinionRenderer);
        
        Destroy(WhiteGlowRenderer);
        Destroy(GreenGlowRenderer);
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        MinionRenderer.DisposeSprite();

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
        // TODO
    }

    private string GetTokenPath()
    {
        if (Minion.Card.Rarity == CardRarity.Legendary)
        {
            return "Sprites/General/Minion_LegendaryToken";
        }

        return "Sprites/General/Minion_NormalToken";
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