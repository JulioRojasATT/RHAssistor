using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BroadCastUnityEventListener : MonoBehaviour {
    
    [SerializeField] public List<ScriptableObject> eventBroadCasters;

    [SerializeField] public List<UnityEvent> events;

    private void Awake() {
        for (var i = 0; i < eventBroadCasters.Count; i++) {
            IEventBroadcaster<EventArgs> interfaced = (IEventBroadcaster<EventArgs>) eventBroadCasters[i];
            if (interfaced!=null) {
                int index = i;
                EventHandler newEventHandler = (sender, args) => events[index].Invoke();
                interfaced.SubscribeGenericEventHandler("Event" + index, newEventHandler);
            }
        }
    }

    private void OnDestroy() {
        for (var i = 0; i < eventBroadCasters.Count; i++) {
            IEventBroadcaster<EventArgs> interfaced = (IEventBroadcaster<EventArgs>) eventBroadCasters[i];
            if (interfaced!=null) {
                interfaced.UnsubscribeGenericEventHandler("Event" + i);
            }
        }
    }
}
