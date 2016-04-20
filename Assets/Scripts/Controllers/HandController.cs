using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player Player;
    public Vector3 Center;

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
        Vector3 separation = new Vector3(100f, 0f, 0f);

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
                            controllerTransform.position = this.Center - separation * (countHalf - i);
                        }
                        else
                        {
                            controllerTransform.position = this.Center + separation * (i + 1 - countHalf);
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
                            controllerTransform.position = this.Center - (separation * (countMiddle - i));
                        }
                        else if (i == countMiddle)
                        {
                            controllerTransform.position = this.Center;
                        }
                        else
                        {
                            controllerTransform.position = this.Center + (separation * (i - countMiddle));
                        }
                    }
                    break;
            }
        }
    }
}