using UnityEngine;

public class HeroController : BaseController
{
    public Hero Hero;

    public SpriteRenderer HeroRenderer;

    public static HeroController Create(Hero hero, Vector3 heroPosition)
    {
        GameObject heroObject = new GameObject("Hero");
        heroObject.transform.position = heroPosition;
        heroObject.transform.localScale = new Vector3(60f, 60f, 60f);
        heroObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        BoxCollider heroCollider = heroObject.AddComponent<BoxCollider>();
        heroCollider.size = new Vector3(2.5f, 3.5f, 0.1f);

        HeroController heroController = heroObject.AddComponent<HeroController>();
        heroController.Hero = hero;

        heroController.Initialize();

        return heroController;
    }

    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, -2);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -1);

        HeroRenderer = CreateRenderer("Hero", Vector3.one, Vector3.zero, 0);

        UpdateSprites();

        HeroRenderer.enabled = true;
    }

    public override void Remove()
    {
        HeroRenderer.DisposeSprite();
        Destroy(HeroRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        HeroRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Loading the sprites
        // TODO : Load hero sprite depending on hero class
        HeroRenderer.sprite = Resources.Load<Sprite>("Sprites/DeathKnight/Hero/DeathKnight_Portrait_Ingame");
        GreenGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Weapon_GreenGlow");
        RedGlowRenderer.sprite = Resources.Load<Sprite>("Sprites/Glows/Weapon_RedGlow");
    }

    #region Unity Messages

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
                enemyPlayer.Controller.SetRedRenderer(true);

                foreach (MinionCard minion in enemyPlayer.Minions)
                {
                    if (minion.Stealth == false)
                    {
                        minion.Controller.SetRedRenderer(true);
                    }
                }
            }

            InterfaceManager.Instance.EnableArrow();
        }
    }

    private void OnMouseUp()
    {
        if (this.Hero.CanAttack())
        {
            InterfaceManager.Instance.DisableArrow();

            Player enemyPlayer = this.Hero.Player.Enemy;

            enemyPlayer.Controller.SetRedRenderer(false);

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