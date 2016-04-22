using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player Player;

    public Vector3 Center;
    public Vector3 Interval = new Vector3(5f, 0f, 0f);
    public Vector3 HalfInterval = new Vector3(2.5f, 0f, 0f);

    public List<CardController> Controllers = new List<CardController>();

    public static HandController Create(Player player, Vector3 handPosition)
    {
        GameObject heroObject = new GameObject("Player_" + player + "_Hand");
        heroObject.transform.position = handPosition;
        heroObject.transform.localScale = new Vector3(50f, 50f, 50f);
        heroObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        HandController handController = heroObject.AddComponent<HandController>();
        handController.Player = player;
        handController.Center = handPosition;

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

            MoveCards();
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
                        Transform controllerTransform = Controllers[i].transform;

                        if (i < countHalf)
                        {
                            int cardPosition = (countHalf - i);
                            controllerTransform.localPosition = -1 * Interval * cardPosition + HalfInterval;
                        }
                        else
                        {
                            int cardPosition = (i + 1 - countHalf);
                            controllerTransform.localPosition = Interval * cardPosition - HalfInterval;
                        }
                    }
                    break;

                case false:
                    int countMiddle = Controllers.Count.Middle() - 1;

                    for (int i = 0; i < Controllers.Count; i++)
                    {
                        Transform controllerTransform = Controllers[i].transform;

                        if (i < countMiddle)
                        {
                            int cardPosition = (countMiddle - i);
                            controllerTransform.localPosition = -1 * Interval * cardPosition;
                        }
                        else if (i == countMiddle)
                        {
                            controllerTransform.localPosition = Vector3.zero;
                        }
                        else
                        {
                            int cardPosition = (i - countMiddle);
                            controllerTransform.localPosition = Interval * cardPosition;
                        }
                    }
                    break;
            }
        }
    }
}