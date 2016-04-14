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
    public static T As<T>(this ICharacter self)
    {
        return (T)self;
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

    #endregion
}