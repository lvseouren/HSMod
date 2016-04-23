using UnityEngine;

public class HeroController : BaseController
{
    public Hero Hero;

    public SpriteRenderer AttackRenderer;
    public SpriteRenderer HealthRenderer;
    public SpriteRenderer HeroRenderer;

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
        AttackRenderer = CreateRenderer("Attack", Vector3.one * 0.55f, new Vector3(-1.5f, -0.75f, 0f), 24);
        HealthRenderer = CreateRenderer("Health", Vector3.one * 0.55f, new Vector3(1.5f, -0.75f, 0f), 24);

        HeroRenderer = CreateRenderer("Hero", Vector3.one, Vector3.zero, 23);

        WhiteGlowRenderer = CreateRenderer("WhiteGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 22);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 21);
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 20);

        UpdateSprites();

        HeroRenderer.enabled = true;
        HealthRenderer.enabled = true;
    }

    public override void Remove()
    {
        HeroRenderer.DisposeSprite();
        Destroy(HeroRenderer);

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
        AttackRenderer.DisposeSprite();
        HealthRenderer.DisposeSprite();
        HeroRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Loading the sprites from the Resources folder
        AttackRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Attack");
        HealthRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Health");
        HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/" + Hero.Class.Name() + "/Hero/" + Hero.Class.Name() + "_Portrait_Ingame");
        WhiteGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_WhiteGlow");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_RedGlow");
    }

    #region Unity Messages

    private void OnMouseEnter()
    {
        this.SetWhiteRenderer(true);

        InterfaceManager.Instance.OnHoverStart(this);
    }

    private void OnMouseExit()
    {
        this.SetWhiteRenderer(false);

        InterfaceManager.Instance.OnHoverStop();
    }

    private void OnMouseDown()
    {
        if (this.Hero.CanAttack())
        {
            Player enemyPlayer = this.Hero.Player.Enemy;

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
        if (this.Hero.CanAttack())
        {
            InterfaceManager.Instance.DisableArrow();

            Player enemyPlayer = this.Hero.Player.Enemy;

            enemyPlayer.HeroController.SetRedRenderer(false);

            foreach (MinionCard minion in enemyPlayer.Minions)
            {
                minion.Controller.SetRedRenderer(false);
            }

            ICharacter target = Util.GetCharacterAtMouse();

            if (target != null && this.Hero.CanTarget(target))
            {
                this.Hero.Attack(target);
            }
        }
    }

    #endregion
}