using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public Player Player;

    private NumberController Controller;
    private List<SpriteRenderer> Crystals;
    
    public static ManaController Create(Player player)
    {
        GameObject manaObject = new GameObject("ManaController");
        manaObject.transform.ChangeParent(player.transform);

        ManaController manaController = manaObject.AddComponent<ManaController>();
        manaController.Player = player;

        return manaController;
    }

    public void DestroyRenderers()
    {
        foreach (SpriteRenderer renderer in Crystals)
        {
            Destroy(renderer);
        }

        Crystals.Clear();
    }

    public void UpdateSprites()
    {
        int count = 0;

        DestroyRenderers();

        for (int i = 0; i < Player.AvailableMana; i++)
        {
            count++;
        }

        for (int i = 0; i < Player.GetUsedMana() - Player.OverloadedMana; i++)
        {
            count++;
        }

        for (int i = 0; i < Player.OverloadedMana; i++)
        {
            count++;
        }
    }

    public void UpdateNumbers()
    {
        
    }
}