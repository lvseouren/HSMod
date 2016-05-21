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
    public Dictionary<string, Sprite> Glows;
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
            {"Minion_Normal", Resources.Load<Sprite>("Sprites/General/Minion_NormalToken")},
            {"Minion_Legendary", Resources.Load<Sprite>("Sprites/General/Minion_LegendaryToken")},
            {"Minion_Taunt", Resources.Load<Sprite>("Sprites/General/Minion_TauntToken")},

            {"HeroPower_Front", Resources.Load<Sprite>("Sprites/General/HeroPower_FrontToken")},
            {"HeroPower_Back", Resources.Load<Sprite>("Sprites/General/HeroPower_BackToken")},

            {"Weapon_Open", Resources.Load<Sprite>("Sprites/General/Weapon_OpenToken")},
            {"Weapon_Closed", Resources.Load<Sprite>("Sprites/General/Weapon_ClosedToken")},
        };

        Glows = new Dictionary<string, Sprite>()
        {
            {"Card_LegendaryMinion_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Card_LegendaryMinion_GreenGlow")},
            {"Card_Minion_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Card_Minion_GreenGlow")},
            {"Card_Spell_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Card_Spell_GreenGlow")},
            {"Card_Weapon_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Card_Weapon_GreenGlow")},

            {"Card_Normal_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Card_Normal_GreenGlow")},
            {"Card_Normal_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Card_Normal_RedGlow")},

            {"Hero_Portrait_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_GreenGlow")},
            {"Hero_Portrait_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_RedGlow")},
            {"Hero_Portrait_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Portrait_WhiteGlow")},

            {"Hero_Power_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Power_GreenGlow")},
            {"Hero_Power_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Power_RedGlow")},
            {"Hero_Power_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Hero_Power_WhiteGlow")},

            {"Minion_Legendary_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_GreenGlow")},
            {"Minion_Legendary_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_RedGlow")},
            {"Minion_Legendary_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Legendary_WhiteGlow")},

            {"Minion_LegendaryTaunt_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_GreenGlow")},
            {"Minion_LegendaryTaunt_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_RedGlow")},
            {"Minion_LegendaryTaunt_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_LegendaryTaunt_WhiteGlow")},

            {"Minion_Normal_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_GreenGlow")},
            {"Minion_Normal_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_RedGlow")},
            {"Minion_Normal_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_Normal_WhiteGlow")},

            {"Minion_NormalTaunt_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_GreenGlow")},
            {"Minion_NormalTaunt_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_RedGlow")},
            {"Minion_NormalTaunt_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Minion_NormalTaunt_WhiteGlow")},

            {"Weapon_GreenGlow", Resources.Load<Sprite>("Sprites/Glows/Weapon_GreenGlow")},
            {"Weapon_RedGlow", Resources.Load<Sprite>("Sprites/Glows/Weapon_RedGlow")},
            {"Weapon_WhiteGlow", Resources.Load<Sprite>("Sprites/Glows/Weapon_WhiteGlow")},
        };

        Numbers = new Dictionary<string, Sprite[]>()
        {
            {"White", Resources.LoadAll<Sprite>("Sprites/General/NumbersWhite")},
            {"Green", Resources.LoadAll<Sprite>("Sprites/General/NumbersGreen")},
            {"Red", Resources.LoadAll<Sprite>("Sprites/General/NumbersRed")}
        };
    }
}