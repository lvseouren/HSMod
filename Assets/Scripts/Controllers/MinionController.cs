using UnityEngine;

public class MinionController : MonoBehaviour
{
    public CardGlow CardGlow;
    public bool HasTaunt;
    public bool CanTarget;

    private SpriteRenderer greenGlowRenderer;
    private SpriteRenderer whiteGlowRenderer;
    private SpriteRenderer redGlowRenderer;

    public static void AddTo(GameObject gameObject, CardGlow cardGlow, bool hasTaunt, bool canTarget)
    {
        MinionController minionController = gameObject.AddComponent<MinionController>();
        minionController.CardGlow = cardGlow;
        minionController.HasTaunt = hasTaunt;
        minionController.CanTarget = canTarget;

        minionController.Initialize();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        whiteGlowRenderer = CreateChildSprite("WhiteGlow", 2);
        greenGlowRenderer = CreateChildSprite("GreenGlow", 1);
        redGlowRenderer = CreateChildSprite("RedGlow", 0);

        UpdateSprites();
    }

    public void Remove()
    {
        whiteGlowRenderer.DisposeSprite();
        Destroy(whiteGlowRenderer);

        greenGlowRenderer.DisposeSprite();
        Destroy(greenGlowRenderer.gameObject);

        redGlowRenderer.DisposeSprite();
        Destroy(redGlowRenderer);
    }

    public void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        whiteGlowRenderer.DisposeSprite();
        greenGlowRenderer.DisposeSprite();
        redGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();

        // Loading the sprites
        whiteGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "WhiteGlow");
        greenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        redGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
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
        whiteGlowRenderer.enabled = true;

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
        whiteGlowRenderer.enabled = false;

        if (InterfaceManager.Instance.IsDragging)
        {
            InterfaceManager.Instance.DisableArrowCircle();
        }
    }

    #endregion
}