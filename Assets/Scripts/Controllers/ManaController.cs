using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public Player Player;

    private NumberController Controller;
    private List<SpriteRenderer> Crystals;
    
    public int MaximumMana = 10;
    public int TurnMana = 0;
    public int OverloadedMana = 0;
    public int AvailableMana;
    
    public static ManaController Create(Player player)
    {
        GameObject manaObject = new GameObject("ManaController");
        manaObject.transform.ChangeParent(player.transform);

        ManaController manaController = manaObject.AddComponent<ManaController>();
        manaController.Player = player;

        return manaController;
    }

    public void UpdateSprites()
    {
        for (int i = 0; i < TurnMana; i++)
        {
            
        }
    }

    public void UpdateNumbers()
    {
        
    }
}