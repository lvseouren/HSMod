using UnityEngine;

public class CardController : BaseController
{
    public BaseCard Card;

    public SpriteRenderer CardRenderer;

    public static CardController Create(BaseCard card)
    {
        GameObject cardObject = new GameObject(card.Name);
        cardObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        cardObject.transform.localScale = Vector3.one * 60f;

        BoxCollider cardCollider = cardObject.AddComponent<BoxCollider>();
        cardCollider.size = new Vector3(3.5f, 5.5f, 0f);

        CardController cardController = cardObject.AddComponent<CardController>();
        cardController.Card = card;

        cardController.Initialize();

        return cardController;
    }
    
    public override void Initialize()
    {
        Vector3 glowSize = Vector3.one * 3f;
        Vector3 glowOffset = new Vector3(0.065f, -0.05f, 0f);

        RedGlowRenderer = CreateRenderer("RedGlow", glowSize, glowOffset, 30);
        GreenGlowRenderer = CreateRenderer("GreenGlow", glowSize, glowOffset, 31);
        BlueGlowRenderer = CreateRenderer("BlueGlow", glowSize, glowOffset, 32);

        CardRenderer = CreateRenderer("Card", Vector3.one, Vector3.zero, 33);
        CardRenderer.enabled = true;

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
        CardRenderer.sprite = Resources.Load<Sprite>("Sprites/" + className + "/Cards/" + Card.TypeName());
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
        switch (Card.GetCardType())
        {
            case CardType.Minion:
            case CardType.Weapon:
                // TODO : Drag card
                break;

            case CardType.Spell:
                InterfaceManager.Instance.EnableArrow(this);
                break;
        }
    }

    private void OnMouseUp()
    {
        switch (Card.GetCardType())
        {
            case CardType.Minion:
            case CardType.Weapon:
                // TODO : Check position and play or not
                break;

            case CardType.Spell:
                InterfaceManager.Instance.DisableArrow();
                break;
        }
    }

    #endregion
}