using UnityEngine;

// WARNING : EventManager is now the event system class. This script should be deleted in the upcoming commits
public class AnotherEventManager : MonoBehaviour
{
    // For now the Event Manager is a singleton which is roughly used for nothing
    // apart from death phase check that could be placed somewhere else for performance reasons 
    // but i will try to expand on the idea and gradually add more functionality to it.
    // 
    // This script will be used to manage various kind of events and effect queues (graphical effects for cards NYI)
    // List of what it does will appear here as we implement more functionality in the mod

    private static AnotherEventManager _newEventManager;

    public static AnotherEventManager instance
    {
        get {
            if (!_newEventManager)
            {
                _newEventManager = FindObjectOfType(typeof(AnotherEventManager)) as AnotherEventManager;
                if (!_newEventManager)
                {
                    Debug.LogError("NO NewEventManager instance found on screen");
                }

            }
            return _newEventManager;
        }
    }


    // Example of usage:
    // On specific card creation/cast/use/whatever we subscribe methods to events in file Events.cs
    // these events should later be called in a specific situation which we specify in the code
    // for example here I assumed that in HS "death" is a state of GAME not a specific MINION
    // this means that every time a game enters "death" state something should happen: 
    // minions should die, deathrattle should trigger, secrets like redemption should go off, effects based on minion death should go off, etc.
    // we need a representation of all this as events in Events.cs, subscribe somewhere to these events and then fire them in specific situations
    void Update()
    {
        if (GameLoop.currentGameState == GameLoop.GameState.DEATH_PHASE)
        {
            Events.OnMinionDeathEvent();
            Events.OnMinionDeathrattleEvent();
        }
    }
}