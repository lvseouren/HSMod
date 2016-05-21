using UnityEngine;

public class HeroPowerController : BaseController
{
    public BaseHeroPower HeroPower;
    
    private SpriteRenderer HeroPowerRenderer;
    public SpriteRenderer FrontTokenRenderer;
    public SpriteRenderer BackTokenRenderer;

    private NumberController CostController;

    private BoxCollider HeroPowerCollider;

    public static HeroPowerController Create(BaseHeroPower heroPower)
    {
        GameObject heroPowerObject = new GameObject("HeroPowerController");
        heroPowerObject.transform.ChangeParentAt(heroPower.Hero.Player.transform, new Vector3(4f, 0.5f, 0f));

        BoxCollider heroPowerCollider = heroPowerObject.AddComponent<BoxCollider>();
        heroPowerCollider.size = new Vector3(3f, 3f, 0.1f);

        HeroPowerController heroPowerController = heroPowerObject.AddComponent<HeroPowerController>();
        heroPowerController.HeroPower = heroPower;
        heroPowerController.HeroPowerCollider = heroPowerCollider;

        heroPowerController.Initialize();

        return heroPowerController;
    }
    
    public override void Initialize()
    {
        CostController = NumberController.Create("Cost_Controller", this.gameObject, new Vector3(-0.05f, 1.1f, 0f), 20, 0.35f);

        FrontTokenRenderer = CreateRenderer("FrontToken_Sprite", Vector3.one, Vector3.zero, 19);
        BackTokenRenderer = CreateRenderer("BackToken_Sprite", Vector3.one, Vector3.zero, 19);

        HeroPowerRenderer = CreateRenderer("HeroPower_Sprite", Vector3.one, Vector3.zero, 18);

        WhiteGlowRenderer = CreateRenderer("WhiteGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.12f, 0f), 17);
        GreenGlowRenderer = CreateRenderer("GreenGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.12f, 0f), 16);
        RedGlowRenderer = CreateRenderer("RedGlow_Sprite", Vector3.one * 2f, new Vector3(0f, -0.12f, 0f), 15);

        FrontTokenRenderer.enabled = true;
        HeroPowerRenderer.enabled = true;

        UpdateSprites();
        UpdateNumbers();
        
        CostController.SetEnabled(true);
    }

    public override void DestroyController()
    {
        Destroy(FrontTokenRenderer);
        Destroy(BackTokenRenderer);
        Destroy(HeroPowerRenderer);
        Destroy(WhiteGlowRenderer);
        Destroy(GreenGlowRenderer);
        Destroy(RedGlowRenderer);

        Destroy(this.gameObject);
    }

    public override void UpdateSprites()
    {
        // Loading the sprites
        FrontTokenRenderer.sprite = SpriteManager.Instance.Tokens["HeroPower_Front"];
        BackTokenRenderer.sprite = SpriteManager.Instance.Tokens["HeroPower_Back"];
        
        HeroPowerRenderer.sprite = Resources.Load<Sprite>("Sprites/" + HeroPower.Class.Name() + "/HeroPower/Token");

        WhiteGlowRenderer.sprite = SpriteManager.Instance.Glows["Hero_Power_WhiteGlow"];
        GreenGlowRenderer.sprite = SpriteManager.Instance.Glows["Hero_Power_GreenGlow"];
        RedGlowRenderer.sprite = SpriteManager.Instance.Glows["Hero_Power_RedGlow"];
    }

    public override void UpdateNumbers()
    {
        if (HeroPower.CurrentCost < HeroPower.BaseCost)
        {
            CostController.UpdateNumber(HeroPower.CurrentCost, "Green");
        }
        else if (HeroPower.CurrentCost == HeroPower.BaseCost)
        {
            CostController.UpdateNumber(HeroPower.CurrentCost, "White");
        }
        else
        {
            CostController.UpdateNumber(HeroPower.CurrentCost, "Red");
        }
    }
    
    #region Unity Messages

    private void OnMouseEnter()
    {
        SetWhiteRenderer(true);
    }

    private void OnMouseExit()
    {
        SetWhiteRenderer(false);
    }

    private void OnMouseDown()
    {
        if (HeroPower.IsAvailable())
        {
            switch (HeroPower.TargetType)
            {
                case TargetType.NoTarget:
                    if (HeroPower.IsAvailable())
                    {
                        HeroPower.Hero.Player.UseHeroPower(null);
                    }
                    break;

                default:
                    InterfaceManager.Instance.EnableArrow(this);
                    break;
            }
        }
    }

    private void OnMouseUp()
    {
        InterfaceManager.Instance.DisableArrow();

        if (HeroPower.IsAvailable())
        {
            if (HeroPower.TargetType != TargetType.NoTarget)
            {
                Character target = Util.GetCharacterAtMouse();
                
                if (target != null)
                {
                    if (HeroPower.CanTarget(target))
                    {
                        HeroPower.Hero.Player.UseHeroPower(target);
                    }
                }
            }
        }
    }

    #endregion
}