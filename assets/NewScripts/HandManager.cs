using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class HandManager : MonoBehaviour {

    // public List<GameObject> cardsInHand = new List<GameObject>();
    
    //This position will be referenced for the calculations.
    public Transform centerPosition;
    public float rotationOffset;
    public float xPositionOffset;
    public float yPositionOffset;

    [HideInInspector]
    public Transform emptySlot;


    void Start()
    {

    }
    

    public void OrganizeHand()
    {
        foreach (Transform card in transform)
        {

        }
    }

}
