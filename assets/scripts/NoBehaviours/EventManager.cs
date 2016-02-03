using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	public static string ON_TURN_START = "ON_TURN_START";
	public static string ON_DRAW_CARD  = "ON_DRAW_CARD" ;
	public static string ON_TURN_PLAY  = "ON_TURN_PLAY" ;
	public static string ON_TURN_END   = "ON_TURN_END"  ;
	public static string ON_MANA_USAGE = "ON_MANA_USAGE";

	private static EventManager eventManager ;

	private Dictionary <string,UnityEvent>  eventDictionary;

	public static EventManager instance {
		get {
			if (!eventManager) {
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager ;
				if (!eventManager){
					Debug.LogError ("Couldn't find script");
				}else {
					eventManager.Init();
				}
			}
			return eventManager;
		}
	}
	void Init () {
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<string, UnityEvent> ();
		}
	}

	public static void StartListening (string EventName , UnityAction listener) {
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (EventName, out thisEvent)) {
			thisEvent.AddListener (listener);
		} else {
			thisEvent = new UnityEvent ();
			thisEvent.AddListener ( listener );
			instance.eventDictionary.Add (EventName , thisEvent );
		}
	}

	public static void StopListening (string EventName , UnityAction listener) {
		if (eventManager == null) {
			return ;
		}

		UnityEvent thisEvent = null ;
		if (instance.eventDictionary.TryGetValue (EventName, out thisEvent)) {
			thisEvent.RemoveListener (listener);

		}
	}

	public static void  TriggerEvent (string EventName) {
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (EventName, out thisEvent)) {
			thisEvent.Invoke ();
		}
	}
}
