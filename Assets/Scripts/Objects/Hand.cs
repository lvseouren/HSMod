using System.Collections.Generic;
using UnityEngine;

// TODO : Move everything to player as a List and manage drawing from there
public class Hand
{
    public Player Player;

    public GameObject HandObject;

    public List<BaseCard> Cards = new List<BaseCard>();

    public int MaxCards;
}