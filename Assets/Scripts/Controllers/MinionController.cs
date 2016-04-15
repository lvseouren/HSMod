using UnityEngine;

public class MinionController : BaseController
{
    public bool HasTaunt;
    public bool CanTarget;

    public static void AddTo(GameObject gameObject, CardGlow cardGlow, bool hasTaunt, bool canTarget)
    {
        MinionController minionController = gameObject.AddComponent<MinionController>();
        minionController.CardGlow = cardGlow;
        minionController.HasTaunt = hasTaunt;
        minionController.CanTarget = canTarget;

        minionController.Initialize();
    }

    public override void Initialize()
    {
        WhiteGlowRenderer = CreateChildSprite("WhiteGlow", 2);
        GreenGlowRenderer = CreateChildSprite("GreenGlow", 1);
        RedGlowRenderer = CreateChildSprite("RedGlow", 0);

        UpdateSprites();
    }

    public override void Remove()
    {
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
        WhiteGlowRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();

        // Loading the sprites
        WhiteGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "WhiteGlow");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
    }

    private string GetGlowString()
    {
        string glowString = "Sprites/Glows/Minion_";

        switch (this.CardGlow)
        {
            case CardGlow.Minion:
                glowString += "Legendary_";
                break;

            case CardGlow.LegendaryMinion:
                glowString += "Normal_";
                break;
        }
        
        if (this.HasTaunt)
        {
            glowString += "Taunt_";
        }

        return glowString;
    }
    
    private SpriteRenderer CreateChildSprite(string name, int order)
    {
        // Creating a GameObject to hold the SpriteRenderer
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = this.transform;
        glowObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        glowObject.transform.localEulerAngles = Vector3.zero;
        glowObject.transform.localScale = Vector3.one * 2f;

        // Creating the SpriteRenderer and adding it to the GameObject
        SpriteRenderer glowRenderer = glowObject.AddComponent<SpriteRenderer>();
        glowRenderer.sortingLayerName = "Minion";
        glowRenderer.sortingOrder = order;
        glowRenderer.enabled = false;

        return glowRenderer;
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

    private void OnMouseDown()
    {
        if (this.CanTarget)
        {
            InterfaceManager.Instance.EnableArrow();
        }
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();
    }

    private void OnMouseExit()
    {
        WhiteGlowRenderer.enabled = false;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.DisableArrowCircle();
        }
    }

    #endregion
}