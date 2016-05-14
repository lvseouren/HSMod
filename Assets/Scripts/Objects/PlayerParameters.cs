using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters
{
    public HeroClass HeroClass;
    public int HeroHealth;
    public int HeroArmor;

    public Vector3 PlayerPosition;

    public Vector3 HandPosition;
    public bool HandInverted;

    public Vector3 ManaPosition;
    public bool DisplayCrystals;

    public Vector3 BoardPosition;

    public Type HeroPower;

    public List<BaseCard> Deck;
}