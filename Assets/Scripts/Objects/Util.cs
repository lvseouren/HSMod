using System;
using UnityEngine;

// Static class to hold useful extension methods
public static class Util
{
    // Method to dispose the sprites and their textures in a SpriteRenderer
    public static void DisposeSprite(this SpriteRenderer renderer)
    {
        if (renderer.sprite != null)
        {
            GameObject.Destroy(renderer.sprite);

            if (renderer.sprite.texture != null)
            {
                GameObject.Destroy(renderer.sprite.texture);
            }
        }
    }

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

    public static bool IsPair(this int number)
    {
        return (number % 2 == 0);
    }

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
    public static ICharacter GetCharacterAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitInfo = Physics.RaycastAll(cameraRay);

        if (hitInfo.Length > 0)
        {
            BaseController controller = hitInfo[0].collider.gameObject.GetComponent<MinionController>();

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
        return Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1000f));
    }
    
    #region ICharacter Extension Methods

    public static bool IsAlive(this ICharacter self)
    {
        return (self.CurrentHealth > 0);
    }

    public static bool IsFriendlyOf(this ICharacter self, ICharacter other)
    {
        if (self.IsHero())
        {
            if (other.IsHero())
            {
                return self == other;
            }
            else
            {
                return self.As<Hero>().Player.Minions.Contains(other.As<MinionCard>());
            }
        }
        else
        {
            if (other.IsHero())
            {
                return other.As<Hero>().Player.Minions.Contains(self.As<MinionCard>());
            }
            else
            {
                return self.As<MinionCard>().Player.Minions.Contains(other.As<MinionCard>());
            }
        }
    }

    public static bool IsEnemyOf(this ICharacter self, ICharacter other)
    {
        return (self.IsFriendlyOf(other) == false);
    }

    public static bool IsHero(this ICharacter self)
    {
        return (self != null && self.GetType() == typeof (Hero));
    }

    public static bool IsMinion(this ICharacter self)
    {
        return (self != null && self.GetType() == typeof(MinionCard));
    }

    public static int GetMissingHealth(this ICharacter self)
    {
        return self.MaxHealth - self.CurrentHealth;
    }

    #endregion
}