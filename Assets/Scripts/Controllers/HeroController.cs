using UnityEngine;

public class HeroController : BaseController
{
    public Hero Hero;

    public SpriteRenderer HeroRenderer;
    public SpriteRenderer AttackRenderer;
    public SpriteRenderer HealthRenderer;
    public SpriteRenderer ArmorRenderer;

    public TextMesh AttackText;
    public TextMesh HealthText;
    public TextMesh ArmorText;

    // TODO : Armor text and sprite

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
        this.AttackText = CreateText("AttackText", new Vector3(-1.5f, -0.755f, 0f), 26);
        this.HealthText = CreateText("HealthText", new Vector3(1.5f, -0.75f, 0f), 26);
        this.ArmorText = CreateText("ArmorText", new Vector3(1.5f, -0.25f, 0f), 26);

        this.AttackRenderer = CreateRenderer("Attack", Vector3.one * 0.55f, new Vector3(-1.5f, -0.75f, 0f), 24);
        this.HealthRenderer = CreateRenderer("Health", Vector3.one * 0.55f, new Vector3(1.5f, -0.75f, 0f), 24);
        this.ArmorRenderer = CreateRenderer("Armor", Vector3.one * 0.55f, new Vector3(1.5f, -0.25f, 0f), 24);
        this.HeroRenderer = CreateRenderer("Hero", Vector3.one, Vector3.zero, 23);

        this.WhiteGlowRenderer = CreateRenderer("WhiteGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 22);
        this.GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 21);
        this.RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, new Vector3(0.04f, 0.75f, 0f), 20);

        this.HeroRenderer.enabled = true;
        this.HealthRenderer.enabled = true;
        this.HealthText.text = "30";

        UpdateSprites();
        UpdateText();
    }

    public override void Remove()
    {
        this.HeroRenderer.DisposeSprite();
        Destroy(this.HeroRenderer);

        this.AttackRenderer.DisposeSprite();
        Destroy(this.AttackRenderer);

        this.HealthRenderer.DisposeSprite();
        Destroy(this.HealthRenderer);

        this.GreenGlowRenderer.DisposeSprite();
        Destroy(this.GreenGlowRenderer.gameObject);

        this.RedGlowRenderer.DisposeSprite();
        Destroy(this.RedGlowRenderer);

        this.WhiteGlowRenderer.DisposeSprite();
        Destroy(this.WhiteGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        this.HeroRenderer.DisposeSprite();
        this.AttackRenderer.DisposeSprite();
        this.HealthRenderer.DisposeSprite();
        this.GreenGlowRenderer.DisposeSprite();
        this.RedGlowRenderer.DisposeSprite();

        // Loading the sprites from the Resources folder
        this.HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/" + this.Hero.Class.Name() + "/Hero/" + this.Hero.Class.Name() + "_Portrait_Ingame");
        this.AttackRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Attack");
        this.HealthRenderer.sprite = Resources.Load<Sprite>("Sprites/General/Health");
        this.WhiteGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_WhiteGlow");
        this.GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_GreenGlow");
        this.RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_RedGlow");
    }

    public void UpdateText()
    {
        if (this.Hero.CurrentAttack > 0)
        {
            this.AttackRenderer.enabled = true;
            this.AttackText.text = this.Hero.CurrentAttack.ToString();
        }
        else
        {
            this.AttackRenderer.enabled = false;
            this.AttackText.text = string.Empty;
        }

        this.HealthText.text = this.Hero.CurrentHealth.ToString();
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