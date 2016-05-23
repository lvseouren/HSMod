using System.Collections.Generic;
using UnityEngine;

public static class RNG
{
    public static bool RandomBool()
    {
        return Random.Range(0, 2) == 0;
    }

    public static T RandomChoice<T>(T first, T second)
    {
        return RandomBool() ? first : second;
    }

    public static int RandomInteger(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public static Character RandomCharacter(List<Character> characters)
    {
        return characters[RandomInteger(0, characters.Count - 1)];
    }

    public static BaseCard RandomCard(List<BaseCard> cards)
    {
        return cards[RandomInteger(0, cards.Count - 1)];
    }
}