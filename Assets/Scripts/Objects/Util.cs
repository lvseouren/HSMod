using System;
using UnityEngine;

// Static class to hold useful extension methods
public static class Util
{
    // Method to cast easily without the need of parenthesis
    public static T As<T>(this object self)
    {
        return (T) self;
    }

    // Method to get the name of an enum value
    public static string Name(this Enum enumValue)
    {
        return Enum.GetName(enumValue.GetType(), enumValue);
    }

    // Method to get the name of the type
    public static string TypeName(this object typeInstance)
    {
        return typeInstance.GetType().Name;
    }

    // Method to know if an integer is odd or even
    public static bool IsPair(this int number)
    {
        return (number % 2 == 0);
    }

    // Method to get the middle of an odd number
    public static int Middle(this int number)
    {
        return (int) Math.Round((number / 2) + 0.5);
    }

    // Method to get the CardType enum value of a card
    public static CardType GetCardType(this BaseCard card)
    {
        Type cardBaseType = card.GetType().BaseType;

        switch (cardBaseType.Name)
        {
            case "MinionCard":
                return CardType.Minion;

            case "SpellCard":
                return CardType.Spell;

            case "WeaponCard":
                return CardType.Weapon;

            default:
                return CardType.None;
        }
    }

    // Method to get the character at the mouse position
    public static Character GetCharacterAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(cameraRay);

        foreach (RaycastHit hit in hits)
        {
            BaseController controller = hit.collider.gameObject.GetComponent<MinionController>();

            if (controller != null)
            {
                switch (controller.GetType().Name)
                {
                    case "MinionController":
                        return controller.As<MinionController>().Minion;

                    case "HeroController":
                        return controller.As<HeroController>().Hero;
                }
            }
        }

        return null;
    }

    // Method to get the position of the mouse in the world space
    public static Vector3 GetWorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1940f));
    }

    // Method to reset a transform
    public static void Reset(this Transform self)
    {
        self.localPosition = Vector3.zero;
        self.localEulerAngles = Vector3.zero;
        self.localScale = Vector3.one;
    }
    
    // Method to reset a transform and set it at the parent origin
    public static void ChangeParent(this Transform self, Transform parent)
    {
        self.parent = parent;
        self.Reset();
    }

    // Method to reset a transform and position it respect another transform
    public static void ChangeParentAt(this Transform self, Transform parent, Vector3 position)
    {
        self.parent = parent;
        self.Reset();
        self.localPosition = position;
    }
}