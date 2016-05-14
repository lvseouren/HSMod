using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player Player;
    public List<CardController> Controllers = new List<CardController>();

    private const float DISTANCE = 10.5f;

    public static HandController Create(Player player, Vector3 handPosition, bool inverted)
    {
        GameObject heroObject = new GameObject("HandController");
        heroObject.transform.ChangeParentAt(player.transform, handPosition);
        heroObject.transform.localScale = Vector3.one * 0.75f;

        if (inverted)
        {
            heroObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }

        HandController handController = heroObject.AddComponent<HandController>();
        handController.Player = player;

        return handController;
    }

    public void Add(CardController cardController)
    {
        Controllers.Add(cardController);

        cardController.transform.parent = this.transform;
        cardController.transform.Reset();

        MoveCards();
    }

    public void Remove(CardController cardController)
    {
        if (Controllers.Contains(cardController))
        {
            Controllers.Remove(cardController);
        }

        MoveCards();
    }
    
    private void MoveCards()
    {
        if (Controllers.Count > 0)
        {
            float separationAngle = (27 - Controllers.Count * 2);

            float halfTotalAngle = separationAngle * (Controllers.Count - 1) / 2f;

            for (int i = 0; i < Controllers.Count; i++)
            {
                CardController controller = Controllers[i];

                float cardAngle = separationAngle * i - halfTotalAngle;
                float cardRotation = cardAngle * Mathf.Deg2Rad;

                float cardX = Mathf.Sin(cardRotation) * DISTANCE;
                float cardY = Mathf.Cos(cardRotation) * DISTANCE;
                    
                controller.TargetPosition = new Vector3(cardX, cardY, 0f);
                controller.TargetRotation = new Vector3(0f, 0f, -cardAngle);

                controller.TargetRenderingOrder = 200 + 10 * i;
            }
        }
    }
}