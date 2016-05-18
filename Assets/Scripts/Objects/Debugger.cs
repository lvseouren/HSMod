using System;
using UnityEngine;

public static class Debugger
{
    public static void Log(string text)
    {
        string time = string.Format("{0:HH:mm:ss}", DateTime.Now);
        MonoBehaviour.print("[" + time + "]" + " " + text);
    }

    public static void LogMinion(Minion minion, string action)
    {
        Log(minion.Card.Name + " " + action);
    }

    public static void LogPlayer(Player player, string action)
    {
        Log(player.Hero.GetName() + " " + action);
    }

    public static void LogHero(Hero hero, string action)
    {
        Log(hero.GetName() + " " + action);
    }
}