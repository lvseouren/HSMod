using System;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardGlow CardGlow;

    private SpriteRenderer greenGlowRenderer;
    private SpriteRenderer blueGlowRenderer;
    private SpriteRenderer redGlowRenderer;

    public static void AddTo(GameObject gameObject)
    {
        CardController cardController = gameObject.AddComponent<CardController>();

        cardController.Initialize();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        blueGlowRenderer = CreateChildSprite("BlueGlow", 2);
        greenGlowRenderer = CreateChildSprite("GreenGlow", 1);
        redGlowRenderer = CreateChildSprite("RedGlow", 0);

        UpdateSprites();
    }

    public void Remove()
    {
        greenGlowRenderer.DisposeSprite();
        Destroy(greenGlowRenderer.gameObject);

        blueGlowRenderer.DisposeSprite();
        Destroy(blueGlowRenderer);

        redGlowRenderer.DisposeSprite();
        Destroy(redGlowRenderer);
    }

    public void UpdateSprites()
    {
        // Cleaning up the old sprites and their textures to avoid memory leaks
        greenGlowRenderer.DisposeSprite();
        blueGlowRenderer.DisposeSprite();
        redGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();
    
        // Loading the sprites into the SpriteRenderers
        greenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        redGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
        blueGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "BlueGlow");
    }

    private string GetGlowString()
    {
        return "Sprites/Glows/Card_" + Enum.GetName(typeof (CardGlow), CardGlow) + "_";
    }

    private SpriteRenderer CreateChildSprite(string name, int order)
    {
        // Creating a GameObject to hold the SpriteRenderer
        GameObject glowObject = new GameObject(name);
        glowObject.transform.parent = this.transform;
        glowObject.transform.localPosition = new Vector3(0.07f, 0f, 0f);
        glowObject.transform.localEulerAngles = Vector3.zero;
        glowObject.transform.localScale = Vector3.one * 3f;

        // Creating the SpriteRenderer and adding it to the GameObject
        SpriteRenderer glowRenderer = glowObject.AddComponent<SpriteRenderer>();
        glowRenderer.sortingLayerName = "Card";
        glowRenderer.sortingOrder = order;
        glowRenderer.enabled = false;

        return glowRenderer;
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        greenGlowRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
    }

    private void OnMouseDown()
    {
        InterfaceManager.Instance.EnableArrow();
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();
    }

    #endregion
}

public enum CardGlow
{
    Normal,
    Spell,
    Minion,
    LegendaryMinion,
    Weapon
}