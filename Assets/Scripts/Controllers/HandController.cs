﻿using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Player Player;

    public Vector3 Center;
    public float Interval = 5f;
    public float HalfInterval = 2.5f;

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
        this.Controllers.Add(cardController);

        cardController.transform.parent = this.transform;

        this.MoveCards();
    }

    public void Remove(CardController cardController)
    {
        if (this.Controllers.Contains(cardController))
        {
            this.Controllers.Remove(cardController);

            this.MoveCards();
        }
    }

    private void MoveCards()
    {
        if (this.Controllers.Count > 0)
        {
            switch (this.Controllers.Count.IsPair())
            {
                case true:
                    int countHalf = (this.Controllers.Count / 2);

                    for (int i = 0; i < this.Controllers.Count; i++)
                    {
                        CardController controller = this.Controllers[i];

                        if (i < countHalf)
                        {
                            int cardPosition = (countHalf - i);
                            controller.TargetX = -1 * this.Interval * cardPosition + this.HalfInterval;
                        }
                        else
                        {
                            int cardPosition = (i + 1 - countHalf);
                            controller.TargetX = this.Interval * cardPosition - this.HalfInterval;
                        }
                    }
                    break;

                case false:
                    int countMiddle = this.Controllers.Count.Middle() - 1;

                    for (int i = 0; i < this.Controllers.Count; i++)
                    {
                        CardController controller = this.Controllers[i];

                        if (i < countMiddle)
                        {
                            int cardPosition = (countMiddle - i);
                            controller.TargetX = -1 * this.Interval * cardPosition;
                        }
                        else if (i == countMiddle)
                        {
                            controller.TargetX = 0f;
                        }
                        else
                        {
                            int cardPosition = (i - countMiddle);
                            controller.TargetX = this.Interval * cardPosition;
                        }
                    }
                    break;
            }
        }
    }
}