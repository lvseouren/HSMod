using UnityEngine;

public class CardController : BaseController
{
    public BaseCard Card;
    public float TargetY = 0f;
    public float TargetX = 0f;

    private SpriteRenderer CardRenderer;
    private SpriteRenderer ComboGlowRenderer;

    private TextMesh CostText;

    private TextMesh AttackText;
    private TextMesh AttributeText;

    private BoxCollider CardCollider;

    private bool IsDragging = false;
    private bool IsTargeting = false;

    public static CardController Create(BaseCard card)
    {
        GameObject cardObject = new GameObject(card.Name);
        cardObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        cardObject.transform.localScale = Vector3.one * 50f;

        CardController controller = cardObject.AddComponent<CardController>();
        controller.Card = card;

        controller.CardCollider = cardObject.AddComponent<BoxCollider>();
        controller.CardCollider.size = new Vector3(3.5f, 5.5f, 0f);

        controller.Initialize();

        return controller;
    }
    
    public override void Initialize()
    {
        Vector3 glowSize = Vector3.one * 3f;
        Vector3 glowOffset = new Vector3(0.065f, -0.05f, 0f);

        CardRenderer = CreateRenderer("Card", Vector3.one, Vector3.zero, 42);
        
        ComboGlowRenderer = CreateRenderer("ComboGlow", glowSize, glowOffset, 41);
        GreenGlowRenderer = CreateRenderer("GreenGlow", glowSize, glowOffset, 40);

        CardRenderer.enabled = true;

        UpdateSprites();
        UpdateText();
    }

    public override void Remove()
    {
        CardRenderer.DisposeSprite();
        Destroy(CardRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        ComboGlowRenderer.DisposeSprite();
        Destroy(ComboGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and their textures to avoid memory leaks
        CardRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        ComboGlowRenderer.DisposeSprite();

        // Getting the string path to the glows
        string glowString = GetGlowString();
        string className = Card.Class.Name();
    
        // Loading the sprites into the SpriteRenderers
        CardRenderer.sprite = Resources.Load<Sprite>("Sprites/" + className + "/Cards/" + Card.TypeName());
        GreenGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "GreenGlow");
        ComboGlowRenderer.sprite = Resources.Load<Sprite>(glowString + "ComboGlow");
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

    private void Update()
    {
        if (this.IsDragging)
        {
            this.transform.position = Util.GetWorldMousePosition();
        }
        else if (this.IsTargeting)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, new Vector3(TargetX, TargetY / 2f), 50f * Time.deltaTime);
        }
        else
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, new Vector3(TargetX, TargetY, 0f), 50f * Time.deltaTime);
        }
    }

    private void OnMouseEnter()
    {
        this.TargetY = 5f;

        if (this.IsDragging == false && this.IsTargeting == false)
        {
            this.transform.localScale = Vector3.one * 2f;
        }
    }

    private void OnMouseExit()
    {
        if (this.IsDragging == false && this.IsTargeting == false)
        {
            this.TargetY = 0f;
            this.transform.localScale = Vector3.one;
        }
    }

    private void OnMouseDown()
    {
        this.transform.localScale = Vector3.one;

        switch (this.Card.GetCardType())
        {
            case CardType.Minion:
            case CardType.Weapon:
                this.IsDragging = true;
                break;

            case CardType.Spell:
                this.IsTargeting = true;
                InterfaceManager.Instance.EnableArrow(this);
                break;
        }
    }

    private void OnMouseUp()
    {
        this.IsDragging = false;
        this.IsTargeting = false;

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

        this.TargetY = 0f;
    }

    #endregion
}