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
        GameObject heroObject = new GameObject("Player_" + player + "_Hand");
        heroObject.transform.position = handPosition;
        heroObject.transform.localScale = Vector3.one * 50f;
        heroObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        HandController handController = heroObject.AddComponent<HandController>();
        handController.Player = player;

        return handController;
    }

    public void Add(CardController cardController)
    {
        Controllers.Add(cardController);

        cardController.transform.parent = this.transform;

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
        }
    }
}