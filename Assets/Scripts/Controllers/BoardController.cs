using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Player Player;

    public List<BoxCollider> BoardColliders = new List<BoxCollider>();

    private Vector3 Center;

    public static BoardController Create(Player player, Vector3 boardCenter)
    {
        GameObject boardObject = new GameObject("BoardController");
        boardObject.transform.ChangeParentAt(player.transform, boardCenter);

        BoardController boardController = boardObject.AddComponent<BoardController>();
        boardController.Player = player;
        boardController.Center = boardCenter;

        boardController.UpdateSlots();

        return boardController;
    }

    public void UpdateSlots()
    {
        DestroySlots();

        int slots = Player.Minions.Count + 1;
        float slotSize = 27f / slots;

        for (int i = 0; i < slots; i++)
        {
            CreateSlot(slotSize, i * slotSize);
        }

        float boardCenter = (slots * slotSize) - slotSize;

        this.transform.localPosition = new Vector3(boardCenter, 0f, 0f) + Center;
    }

    public void DestroySlots()
    {
        foreach (BoxCollider slotCollider in BoardColliders)
        {
            Destroy(slotCollider.gameObject);
        }

        BoardColliders.Clear();
    }

    public BoxCollider CreateSlot(float width, float horizontalPosition)
    {
        GameObject slot = new GameObject("Slot" + BoardColliders.Count);
        slot.transform.ChangeParentAt(this.transform, new Vector3(horizontalPosition, 0f, 0f));

        BoxCollider slotCollider = slot.AddComponent<BoxCollider>();
        slotCollider.size = new Vector3(width, 4f, 1f);

        return slotCollider;
    }
}