using UnityEngine;

public class CardController : BaseController
{
    public BaseCard Card;
    public float TargetY = 0f;
    public float TargetX = 0f;

    private SpriteRenderer CardRenderer;
    private SpriteRenderer ComboGlowRenderer;

    public NumberController CostController;
    public NumberController AttackController;
    public NumberController AttributeController;

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
        CostController = NumberController.Create("CostController", this.gameObject, new Vector3(1.5f, -0.85f, 0f), 43);
        AttackController = NumberController.Create("AttackController", this.gameObject, new Vector3(-1.4f, -0.85f, 0f), 43);
        AttributeController = NumberController.Create("AttributeController", this.gameObject, new Vector3(1.5f, 0f, 0f), 43);

        CardRenderer = CreateRenderer("Card", Vector3.one, Vector3.zero, 42);
        
        ComboGlowRenderer = CreateRenderer("ComboGlow", Vector3.one * 3f, new Vector3(0.065f, -0.05f, 0f), 41);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 3f, new Vector3(0.065f, -0.05f, 0f), 40);

        CardRenderer.enabled = true;

        UpdateSprites();
        UpdateNumbers();
    }

    public override void Remove()
    {
        CardRenderer.DisposeSprite();
        Destroy(CardRenderer);

        Destroy(GreenGlowRenderer);
        Destroy(ComboGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Getting the string path to the glows
        string glowType = GetGlowType();
    
        // Loading the sprites into the SpriteRenderers
        CardRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Card.Class.Name() + "/Cards/" + Card.TypeName());
        GreenGlowRenderer.sprite = SpriteManager.Instance.Glows["Card_" + glowType + "_GreenGlow"];
        ComboGlowRenderer.sprite = SpriteManager.Instance.Glows["Card_" + glowType + "_ComboGlow"];
    }

    public override void UpdateNumbers()
    {
        if (Card.CurrentCost < Card.BaseCost)
        {
            CostController.UpdateNumber(Card.CurrentCost, "Green");
        }
        else if (Card.CurrentCost == Card.BaseCost)
        {
            CostController.UpdateNumber(Card.CurrentCost, "White");
        }
        else
        {
            CostController.UpdateNumber(Card.CurrentCost, "Red");
        }
    }

    private string GetGlowType()
    {
        switch (Card.TypeName())
        {
            case "MinionCard":
                if (Card.As<MinionCard>().Rarity == CardRarity.Legendary)
                {
                    return "LegendaryMinion";
                }
                return "Minion";

            case "SpellCard":
                return "Spell";

            case "WeaponCard":
                return "Weapon";

            default:
                return "Normal";
        }
    }

    #region Unity Messages

    private void Update()
    {
        if (IsDragging)
        {
            transform.position = Util.GetWorldMousePosition();
        }
        else if (IsTargeting)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(TargetX, TargetY / 2f), 50f * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(TargetX, TargetY, 0f), 50f * Time.deltaTime);
        }
    }

    private void OnMouseEnter()
    {
        TargetY = 4f;

        if (IsDragging == false && IsTargeting == false)
        {
            transform.localScale = Vector3.one * 2f;
        }
    }

    private void OnMouseExit()
    {
        if (IsDragging == false && IsTargeting == false)
        {
            TargetY = 0f;
            transform.localScale = Vector3.one;
        }
    }

    private void OnMouseDown()
    {
        transform.localScale = Vector3.one;

        switch (Card.GetCardType())
        {
            case CardType.Minion:
            case CardType.Weapon:
                IsDragging = true;
                break;

            case CardType.Spell:
                IsTargeting = true;
                InterfaceManager.Instance.EnableArrow(this);
                break;
        }
    }

    private void OnMouseUp()
    {
        IsDragging = false;
        IsTargeting = false;

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

        TargetY = 0f;
    }

    #endregion
}