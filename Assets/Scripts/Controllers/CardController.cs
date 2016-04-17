using System;
using UnityEngine;

public class CardController : BaseController
{
    public CardGlow CardGlow;

    public BaseCard Card;

    public SpriteRenderer CardRenderer;

    public static void Create(BaseCard card, CardGlow cardGlow)
    {
        GameObject cardObject = new GameObject(card.Player.name + "_" + card.Name);

        CardController cardController = cardObject.AddComponent<CardController>();
        cardController.CardGlow = cardGlow;
        cardController.Card = card;

        cardController.Initialize();
    }
    
    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one, Vector3.zero, -3);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one, Vector3.zero, -2);
        BlueGlowRenderer = CreateRenderer("BlueGlow", Vector3.one, Vector3.zero, -1);

        CardRenderer = CreateRenderer("Card", Vector3.one, Vector3.zero, 0);

        UpdateSprites();
    }

    public override void Remove()
    {
        CardRenderer.DisposeSprite();
        Destroy(CardRenderer);

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

    #region Unity Messages

    private void OnMouseEnter()
    {
        GreenGlowRenderer.enabled = true;

        // TODO : Move up
    }

    private void OnMouseExit()
    {
        // TODO : Move down
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