using UnityEngine;

public class HeroController : BaseController
{
    public Hero Hero;

    public SpriteRenderer HeroRenderer;

    public SpriteRenderer AttackRenderer;
    public SpriteRenderer HealthRenderer;
    public SpriteRenderer ArmorRenderer;

    public NumberController AttackController;
    public NumberController HealthController;
    public NumberController ArmorController;

    public static HeroController Create(Hero hero, Vector3 heroPosition)
    {
        GameObject heroObject = new GameObject("Hero_" + hero.Class);
        heroObject.transform.position = heroPosition;
        heroObject.transform.localScale = new Vector3(50f, 50f, 50f);
        heroObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        BoxCollider heroCollider = heroObject.AddComponent<BoxCollider>();
        heroCollider.center = new Vector3(0f, 0.75f, 0f);
        heroCollider.size = new Vector3(4f, 4f, 0.1f);

        HeroController heroController = heroObject.AddComponent<HeroController>();
        heroController.Hero = hero;

        heroController.Initialize();

        return heroController;
    }

    public override void Initialize()
    {
        AttackController = NumberController.Create("AttackController", this.gameObject, new Vector3(-1.4f, -0.85f, 0f), 36);
        HealthController = NumberController.Create("HealthController", this.gameObject, new Vector3(1.5f, -0.85f, 0f), 36);
        ArmorController = NumberController.Create("ArmorController", this.gameObject, new Vector3(1.5f, 0f, 0f), 36);

        AttackRenderer = CreateRenderer("Attack", Vector3.one * 0.55f, new Vector3(-1.5f, -0.75f, 0f), 34);
        HealthRenderer = CreateRenderer("Health", Vector3.one * 0.55f, new Vector3(1.5f, -0.75f, 0f), 34);
        ArmorRenderer = CreateRenderer("Armor", Vector3.one * 0.55f, new Vector3(1.5f, 0.75f, 0f), 34);

        HeroRenderer = CreateRenderer("Hero", Vector3.one, Vector3.zero, 33);

        WhiteGlowRenderer = CreateRenderer("WhiteGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 32);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 31);
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 30);

        HeroRenderer.enabled = true;
        HealthRenderer.enabled = true;

        UpdateSprites();
        UpdateNumbers();
        HealthController.SetEnabled(true);
    }

    public override void Remove()
    {
        HeroRenderer.DisposeSprite();
        Destroy(HeroRenderer);

        AttackRenderer.DisposeSprite();
        Destroy(AttackRenderer);

        HealthRenderer.DisposeSprite();
        Destroy(HealthRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);

        WhiteGlowRenderer.DisposeSprite();
        Destroy(WhiteGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        HeroRenderer.DisposeSprite();
        AttackRenderer.DisposeSprite();
        HealthRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Loading the sprites from the Resources folder
        HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Hero.Class.Name() + "/Hero/" + Hero.Class.Name() + "_Portrait_Ingame");
        AttackRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Attack");
        HealthRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Health");
        WhiteGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_WhiteGlow");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_RedGlow");
    }

    public override void UpdateNumbers()
    {
        // Updating Hero Attack
        if (Hero.CurrentAttack > 0)
        {
            AttackController.UpdateNumber(Hero.CurrentAttack, "White");

            AttackRenderer.enabled = true;
            AttackController.SetEnabled(true);
        }
        else
        {
            AttackRenderer.enabled = false;
            AttackController.SetEnabled(false);
        }

        // Updating Hero Armor
        if (Hero.CurrentArmor > 0)
        {
            ArmorController.UpdateNumber(Hero.CurrentArmor, "White");

            ArmorRenderer.enabled = true;
            ArmorController.SetEnabled(true);
        }
        else
        {
            ArmorRenderer.enabled = false;
            ArmorController.SetEnabled(false);
        }

        // Updating Hero Health
        if (Hero.CurrentHealth == Hero.MaxHealth)
        {
            HealthController.UpdateNumber(Hero.CurrentHealth, "White");
        }
        else
        {
            HealthController.UpdateNumber(Hero.CurrentHealth, "Red");
        }
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        SetWhiteRenderer(true);

        InterfaceManager.Instance.OnHoverStart(this);
    }

    private void OnMouseExit()
    {
        SetWhiteRenderer(false);

        InterfaceManager.Instance.OnHoverStop();
    }

    private void OnMouseDown()
    {
        if (Hero.CanAttack())
        {
            Player enemyPlayer = Hero.Player.Enemy;

            // Adding red glows to available targets
            if (enemyPlayer.HasTauntMinions())
            {
                foreach (MinionCard minion in enemyPlayer.Minions)
                {
                    if (minion.Taunt && minion.Stealth == false)
                    {
                        minion.Controller.SetRedRenderer(true);
                    }
                }
            }
            else
            {
                enemyPlayer.HeroController.SetRedRenderer(true);

                foreach (MinionCard minion in enemyPlayer.Minions)
                {
                    if (minion.Stealth == false)
                    {
                        minion.Controller.SetRedRenderer(true);
                    }
                }
            }

            InterfaceManager.Instance.EnableArrow(this);
        }
    }

    private void OnMouseUp()
    {
        if (Hero.CanAttack())
        {
            InterfaceManager.Instance.DisableArrow();

            Player enemyPlayer = Hero.Player.Enemy;

            enemyPlayer.HeroController.SetRedRenderer(false);

            foreach (MinionCard minion in enemyPlayer.Minions)
            {
                minion.Controller.SetRedRenderer(false);
            }

            ICharacter target = Util.GetCharacterAtMouse();

            if (target != null && Hero.CanTarget(target))
            {
                Hero.Attack(target);
            }
        }
    }

    #endregion
}