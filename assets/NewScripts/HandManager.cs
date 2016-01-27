using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class HandManager : MonoBehaviour {

    public List<GameObject> cardsInHand = new List<GameObject>();

    public float rotationOffset;
    public float xPositionOffset;
    public float yPositionOffset;

    public Transform emptySlot;

    void Update()
    {
        foreach(GameObject card in cardsInHand)
        {
            //Needs something to auto arrange cards in hand
        }
    }

}
