using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Player Player;

    private List<MinionController> MinionControllers = new List<MinionController>();

    private Vector3 Center;
    private const float DISTANCE = 3f;

    public static BoardController Create(Player player, Vector3 boardCenter)
    {
        GameObject boardObject = new GameObject("BoardController");
        boardObject.transform.ChangeParentAt(player.transform, boardCenter);

        BoxCollider boardCollider = boardObject.AddComponent<BoxCollider>();
        boardCollider.size = new Vector3(27f, 5f, 0.1f);

        BoardController boardController = boardObject.AddComponent<BoardController>();
        boardController.Player = player;
        boardController.Center = boardCenter;

        boardController.UpdateBoard();

        return boardController;
    }

    public void AddMinion(Minion minion, int position)
    {
        MinionControllers.Add(minion.Controller.As<MinionController>());

        UpdateBoard();
    }

    public void RemoveMinion(Minion minion)
    {
        MinionControllers.Remove(minion.Controller.As<MinionController>());

        UpdateBoard();
    }

    public void UpdateBoard()
    {
        if (MinionControllers.Count > 0)
        {
            float parentOffset = ((0.5f * MinionControllers.Count) - 0.5f) * -DISTANCE;

            for (int i = 0; i < MinionControllers.Count; i++)
            {
                MinionControllers[i].TargetPosition = new Vector3(i * DISTANCE, 0f, 0f);
            }

            this.transform.localPosition = new Vector3(parentOffset, 0f, 0f) + Center;
        }
        else
        {
            this.transform.localPosition = Center;
        }
    }

    public bool ContainsPoint(Vector3 point)
    {
        Vector3 localPoint = transform.InverseTransformPoint(point);

        if (localPoint.x < 13.5f && localPoint.x > -13.5f)
        {
            if (localPoint.y < 2f && localPoint.y > -2f)
            {
                return true;
            }
        }

        return false;
    }
}