using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    #region Singleton

    private static SpriteManager _instance;

    public static SpriteManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SpriteManager();
            }
            return _instance;
        }
    }

    private SpriteManager() { }

    #endregion

    public Dictionary<string, Sprite> Attributes;
    public Dictionary<string, Sprite> Crystals;
    public Dictionary<string, Sprite> Effects;
    public Dictionary<string, Sprite> Tokens;
    public Dictionary<string, Sprite[]> Numbers;

    private void Awake()
    {
        _instance = this;

        Attributes = new Dictionary<string, Sprite>()
        {
            {"Attack", Resources.Load<Sprite>("Sprites/General/Attack")},
            {"Health", Resources.Load<Sprite>("Sprites/General/Health")},
            {"Armor", Resources.Load<Sprite>("Sprites/General/Armor")}
        };

        Crystals = new Dictionary<string, Sprite>()
        {
            {"Available", Resources.Load<Sprite>("Sprites/General/AvailableCrystal")},
            {"Used", Resources.Load<Sprite>("Sprites/General/UsedCrystal")},
            {"Overloaded", Resources.Load<Sprite>("Sprites/General/OverloadedCrystal")}
        };

        Effects = new Dictionary<string, Sprite>()
        {
            {"Trigger", Resources.Load<Sprite>("Sprites/General/Trigger")},
            {"DivineShield", Resources.Load<Sprite>("Sprites/General/DivineShield")},
            {"Deathrattle", Resources.Load<Sprite>("Sprites/General/Deathrattle")}
        };

        Tokens = new Dictionary<string, Sprite>()
        {
            {"NormalToken", Resources.Load<Sprite>("Sprites/General/NormalToken")},
            {"LegendaryToken", Resources.Load<Sprite>("Sprites/General/LegendaryToken")},
            {"TauntToken", Resources.Load<Sprite>("Sprites/General/TauntToken")}
        };

        Numbers = new Dictionary<string, Sprite[]>()
        {
            {"White", Resources.LoadAll<Sprite>("Sprites/General/NumbersWhite")},
            {"Green", Resources.LoadAll<Sprite>("Sprites/General/NumbersGreen")},
            {"Red", Resources.LoadAll<Sprite>("Sprites/General/NumbersRed")}
        };
    }
}