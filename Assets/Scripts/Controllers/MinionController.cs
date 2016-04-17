using UnityEngine;

public class MinionController : BaseController
{
    public MinionCard Minion;

    public SpriteRenderer MinionRenderer;
    public SpriteRenderer TokenRenderer;

    public bool HasTaunt;

    public bool CanTarget = true;

    public static void Create(MinionCard minion)
    {
        GameObject minionObject = new GameObject(minion.Player.name + "_" + minion.Name);

        MinionController minionController = minionObject.AddComponent<MinionController>();
        minionController.HasTaunt = minion.Taunt;

        minionController.Initialize();
    }

    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, -3);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -2);
        WhiteGlowRenderer = CreateRenderer("WhiteGlow", Vector3.one * 2f, Vector3.zero, -1);

        MinionRenderer = CreateRenderer("Minion", Vector3.one, Vector3.zero, 0);

        TokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 1);
        
        UpdateSprites();
    }

    public override void Remove()
    {
        MinionRenderer.DisposeSprite();
        Destroy(MinionRenderer);

        TokenRenderer.DisposeSprite();
        Destroy(TokenRenderer);

        WhiteGlowRenderer.DisposeSprite();
        Destroy(WhiteGlowRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        TokenRenderer.DisposeSprite();
        WhiteGlowRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Getting the path strings
        string tokenString = GetTokenString();
        string glowString = GetGlowString();

        // Loading the sprites
        TokenRenderer.sprite = Resources.Load<Sprite>(tokenString + this.Minion.TypeName());
        WhiteGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "WhiteGlow");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
    }

    private string GetTokenString()
    {
        return "Sprites/" + this.Minion.Class.Name() + "/Minions/";
    }

    private string GetGlowString()
    {
        string glowString = "Sprites/Glows/Minion_";

        switch (this.Minion.Rarity)
        {
            case CardRarity.Legendary:
                glowString += "Legendary_";
                break;

            default:
                glowString += "Normal_";
                break;
        }
        
        if (this.HasTaunt)
        {
            glowString += "Taunt_";
        }

        return glowString;
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        WhiteGlowRenderer.enabled = true;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.EnableArrowCircle();
        }
    }

    private void OnMouseExit()
    {
        WhiteGlowRenderer.enabled = false;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.DisableArrowCircle();
        }
    }

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