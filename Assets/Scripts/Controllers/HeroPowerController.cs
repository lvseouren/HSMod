using UnityEngine;

public class HeroPowerController : BaseController
{
    public BaseHeroPower HeroPower;
    
    public SpriteRenderer HeroPowerRenderer;
    public SpriteRenderer FrontTokenRenderer;
    public SpriteRenderer BackTokenRenderer;

    public static void Create(BaseHeroPower heroPower)
    {
        GameObject heroPowerObject = new GameObject(heroPower.Hero.Player.name + "_" + heroPower.Name);

        HeroPowerController heroPowerController = heroPowerObject.AddComponent<HeroPowerController>();
        heroPowerController.HeroPower = heroPower;

        heroPowerController.Initialize();
    }
    
    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, -3);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -2);
        WhiteGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -1);

        HeroPowerRenderer = CreateRenderer("Weapon", Vector3.one, Vector3.zero, 0);

        FrontTokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 1);
        BackTokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 2);

        UpdateSprites();
    }

    public override void Remove()
    {
        HeroPowerRenderer.DisposeSprite();
        Destroy(HeroPowerRenderer);

        FrontTokenRenderer.DisposeSprite();
        Destroy(FrontTokenRenderer);

        BackTokenRenderer.DisposeSprite();
        Destroy(BackTokenRenderer);

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
        FrontTokenRenderer.DisposeSprite();
        BackTokenRenderer.DisposeSprite();
        WhiteGlowRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();
        
        // Loading the sprites
        FrontTokenRenderer.sprite = Resources.Load<Sprite>("Sprites/General/HeroPowerFront");
        BackTokenRenderer.sprite = Resources.Load<Sprite>("Sprites/General/HeroPowerBack");
        WhiteGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Power_WhiteGlow");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Power_GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Power_RedGlow");
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        this.SetWhiteRenderer(true);
    }

    private void OnMouseExit()
    {
        this.SetWhiteRenderer(false);
    }

    private void OnMouseDown()
    {
        if (this.HeroPower.IsAvailable())
        {
            switch (this.HeroPower.TargetType)
            {
                case TargetType.NoTarget:
                    HeroPower.Use();
                    break;

                default:
                    InterfaceManager.Instance.EnableArrow();
                    break;
            }
        }
    }

    private void OnMouseUp()
    {
        if (this.HeroPower.IsAvailable())
        {
            if (this.HeroPower.TargetType != TargetType.NoTarget)
            {
                InterfaceManager.Instance.DisableArrow();

                ICharacter target = Util.GetCharacterAtMouse();
                
                if (target != null)
                {
                    if (this.HeroPower.CanTarget(target))
                    {
                        this.HeroPower.Use(target);
                    }
                }
            }
        }
    }

    #endregion
}