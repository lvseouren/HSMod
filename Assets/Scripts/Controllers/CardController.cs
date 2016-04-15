using System;
using UnityEngine;

public class CardController : BaseController
{
    public static void AddTo(GameObject gameObject, CardGlow cardGlow)
    {
        CardController cardController = gameObject.AddComponent<CardController>();
        cardController.CardGlow = cardGlow;

        cardController.Initialize();
    }
    
    public override void Initialize()
    {
        BlueGlowRenderer = CreateChildSprite("BlueGlow", 2);
        GreenGlowRenderer = CreateChildSprite("GreenGlow", 1);
        RedGlowRenderer = CreateChildSprite("RedGlow", 0);

        UpdateSprites();
    }

    public override void Remove()
    {
        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        BlueGlowRenderer.DisposeSprite();
        Destroy(BlueGlowRenderer);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and their textures to avoid memory leaks
        GreenGlowRenderer.DisposeSprite();
        BlueGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();
    
        // Loading the sprites into the SpriteRenderers
        GreenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
        BlueGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "BlueGlow");
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
        GreenGlowRenderer.enabled = true;
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