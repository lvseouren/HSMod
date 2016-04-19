using UnityEngine;

public class CardController : BaseController
{
    public BaseCard Card;

    public SpriteRenderer CardRenderer;

    public static CardController Create(BaseCard card)
    {
        GameObject cardObject = new GameObject(card.Player.name + "_" + card.Name);

        CardController cardController = cardObject.AddComponent<CardController>();
        cardController.Card = card;

        cardController.Initialize();

        return cardController;
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
        CardRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        BlueGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();
        string className = Card.Class.Name();
    
        // Loading the sprites into the SpriteRenderers
        CardRenderer.sprite = Resources.Load<Sprite>("Sprites/" + className + "/Cards/" + Card.Name);
        GreenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "RedGlow");
        BlueGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "BlueGlow");
    }

    private string GetGlowString()
    {
        string glowString = "Sprites/Glows/Card_";

        switch (Card.TypeName())
        {
            case "MinionCard":
                if (Card.As<MinionCard>().Rarity == CardRarity.Legendary)
                {
                    return glowString + "LegendaryMinion_";
                }
                return glowString + "Minion_";

            case "SpellCard":
                return glowString + "Spell_";

            case "WeaponCard":
                return glowString + "Weapon_";

            default:
                return glowString + "Normal_";
        }
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        // TODO : Move up
    }

    private void OnMouseExit()
    {
        // TODO : Move down
    }

    private void OnMouseDown()
    {
        InterfaceManager.Instance.EnableArrow(this.transform.position);
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();
    }

    #endregion
}