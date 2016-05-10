using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player Player;
    public List<CardController> Controllers = new List<CardController>();

    private const float INTERVAL = 4f;
    private const float HALF_INTERVAL = 2f;

    public static HandController Create(Player player, Vector3 handPosition)
    {
        GameObject heroObject = new GameObject("HandController");
        heroObject.transform.ChangeParentAt(player.transform, handPosition);
        heroObject.transform.localScale = Vector3.one * 0.75f;

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

    // TODO : Rewrite
    private void MoveCards()
    {
        if (Controllers.Count > 0)
        {
            switch (Controllers.Count.IsPair())
            {
                case true:
                    int countHalf = (Controllers.Count / 2);

                    for (int i = 0; i < Controllers.Count; i++)
                    {
                        CardController controller = Controllers[i];

                        if (i < countHalf)
                        {
                            int cardPosition = (countHalf - i);
                            controller.TargetX = -1 * INTERVAL * cardPosition + HALF_INTERVAL;
                        }
                        else
                        {
                            int cardPosition = (i + 1 - countHalf);
                            controller.TargetX = INTERVAL * cardPosition - HALF_INTERVAL;
                        }
                    }
                    break;

                case false:
                    int countMiddle = Controllers.Count.Middle() - 1;

                    for (int i = 0; i < Controllers.Count; i++)
                    {
                        CardController controller = Controllers[i];

                        if (i < countMiddle)
                        {
                            int cardPosition = (countMiddle - i);
                            controller.TargetX = -1 * INTERVAL * cardPosition;
                        }
                        else if (i == countMiddle)
                        {
                            controller.TargetX = 0f;
                        }
                        else
                        {
                            int cardPosition = (i - countMiddle);
                            controller.TargetX = INTERVAL * cardPosition;
                        }
                    }
                    break;
            }

            // Setting the rotation of each card
            for (int i = 0; i < Controllers.Count; i++)
            {
                float angle = Mathf.Atan(Controllers[i].TargetX / -50f) * Mathf.Rad2Deg;

                Controllers[i].TargetRotation = new Vector3(0f, 0f, angle);
            }
        }
    }
}