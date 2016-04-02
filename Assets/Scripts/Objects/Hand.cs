using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public Player Player;

    public GameObject HandObject;

    public List<BaseCard> Cards = new List<BaseCard>();

    public int MaxCards;
}