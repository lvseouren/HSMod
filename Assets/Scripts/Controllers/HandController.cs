using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Vector3 Center;

    public List<CardController> Controllers = new List<CardController>();

    public void Add(CardController cardController)
    {
        Controllers.Add(cardController);
    }

    public void Remove(CardController cardController)
    {
        if (Controllers.Contains(cardController))
        {
            Controllers.Remove(cardController);

            MoveCards();
        }
    }

    private void Update()
    {
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