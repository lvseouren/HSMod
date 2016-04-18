using UnityEngine;

public class HeroController : BaseController
{
    public Hero Hero;

    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer TokenRenderer;

    public static HeroController Create(Hero hero)
    {
        GameObject heroObject = new GameObject(hero.Player.name + "_" + hero);
        heroObject.AddComponent<BoxCollider>();

        HeroController heroController = heroObject.AddComponent<HeroController>();
        heroController.Hero = hero;

        heroController.Initialize();

        return heroController;
    }

    public override void Initialize()
    {
        RedGlowRenderer = CreateRenderer("RedGlow", Vector3.one * 2f, Vector3.zero, -2);
        GreenGlowRenderer = CreateRenderer("GreenGlow", Vector3.one * 2f, Vector3.zero, -1);

        WeaponRenderer = CreateRenderer("Weapon", Vector3.one, Vector3.zero, 0);

        TokenRenderer = CreateRenderer("Token", Vector3.one, Vector3.zero, 1);
    }

    public override void Remove()
    {
        WeaponRenderer.DisposeSprite();
        Destroy(WeaponRenderer);

        TokenRenderer.DisposeSprite();
        Destroy(TokenRenderer);

        GreenGlowRenderer.DisposeSprite();
        Destroy(GreenGlowRenderer.gameObject);

        RedGlowRenderer.DisposeSprite();
        Destroy(RedGlowRenderer);
    }

    public override void UpdateSprites()
    {
        // Cleaning up the old sprites and textures to avoid memory leaks
        TokenRenderer.DisposeSprite();
        GreenGlowRenderer.DisposeSprite();
        RedGlowRenderer.DisposeSprite();

        // Loading the sprites
        TokenRenderer.sprite = Resources.Load<Sprite>("Sprites/General/WeaponToken");
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