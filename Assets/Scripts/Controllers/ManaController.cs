using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public Player Player;

    private NumberController Controller;
    private List<SpriteRenderer> Crystals = new List<SpriteRenderer>();

    private bool DisplayCrystals;
    
    public static ManaController Create(Player player, Vector3 manaPosition, bool displayCrystals)
    {
        GameObject manaObject = new GameObject("ManaController");
        manaObject.transform.ChangeParentAt(player.transform, manaPosition);
        manaObject.transform.localScale = Vector3.one * 0.75f;

        ManaController manaController = manaObject.AddComponent<ManaController>();
        manaController.Player = player;
        manaController.DisplayCrystals = displayCrystals;

        manaController.UpdateSprites();
        manaController.UpdateNumbers();

        return manaController;
    }

    public void DestroyController()
    {
        DestroyRenderers();

        Destroy(this.gameObject);
    }

    public void DestroyRenderers()
    {
        foreach (SpriteRenderer renderer in Crystals)
        {
            Destroy(renderer);
        }

        Crystals.Clear();
    }

    // TODO : Next turn overloaded mana
    public void UpdateSprites()
    {
        if (DisplayCrystals)
        {
            int count = 0;

            DestroyRenderers();

            for (int i = 0; i < Player.AvailableMana; i++)
            {
                SpriteRenderer manarRenderer = CreateManaRenderer("Available", new Vector3(count, 0f, 0f));

                Crystals.Add(manarRenderer);

                count++;
            }

            for (int i = 0; i < Player.TurnMana - Player.CurrentOverloadedMana - Player.AvailableMana; i++)
            {
                SpriteRenderer manarRenderer = CreateManaRenderer("Used", new Vector3(count, 0f, 0f));

                Crystals.Add(manarRenderer);

                count++;
            }

            for (int i = 0; i < Player.CurrentOverloadedMana; i++)
            {
                SpriteRenderer manarRenderer = CreateManaRenderer("Overloaded", new Vector3(count, 0f, 0f));

                Crystals.Add(manarRenderer);

                count++;
            }
        }
    }

    public void UpdateNumbers()
    {
        // TODO
    }

    public void UpdateAll()
    {
        UpdateNumbers();
        UpdateSprites();
    }

    private SpriteRenderer CreateManaRenderer(string manaType, Vector3 position)
    {
        GameObject baseObject = new GameObject("ManaCrystal_" + manaType + "_Sprite");
        baseObject.transform.ChangeParentAt(this.transform, position);

        SpriteRenderer spriteRenderer = baseObject.AddComponent<SpriteRenderer>();
        spriteRenderer.material = Resources.Load<Material>("Materials/SpriteOverrideMaterial");
        spriteRenderer.sprite = SpriteManager.Instance.Crystals[manaType];
        spriteRenderer.sortingLayerName = "Game";
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.enabled = true;

        return spriteRenderer;
    }
}