using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EventBroadCaster<T> : ScriptableObject, IEventBroadcaster<T> where T:EventArgs  {
    
    private HashSet<EventHandler<T>> eventListeners = new HashSet<EventHandler<T>>();

    private Dictionary<string, EventHandler> genericEventHandlers;

    /// <summary>
    /// Broadcasts the event to all the registered listeners
    /// </summary>
    public void BroadCast(Object requester, T request) {
        foreach (EventHandler<T> eventListener in eventListeners) {
            eventListener.Invoke(requester,request);
        }

        if (genericEventHandlers == null) {
            return;
        }

        foreach (EventHandler eventHandler in genericEventHandlers.Values) {
            eventHandler.Invoke(requester,request);
        }
    }

    public void BroadCastEmpty()
    {
        BroadCast(null,null);
    }

    public void Subscribe(EventHandler<T> newListener) {
        if (!eventListeners.Contains(newListener)) {
            eventListeners.Add(newListener);            
        }
    }
    
    public void Unsubscribe(EventHandler<T> newListener) {
        if(eventListeners.Contains(newListener)) {
            eventListeners.Remove(newListener);
        }
    }

    public void ClearListeners() {
        eventListeners.Clear();
    }

    private void OnDisable() {
        ClearListeners();
    }

    public void A(EventHandler<T> a) {
        Subscribe(a);
    }
    
    public void SubscribeGenericEventHandler(string label, EventHandler newListener) {
        if (genericEventHandlers==null) {
            genericEventHandlers = new Dictionary<string, EventHandler>();
        }
        genericEventHandlers.TryAdd(label, newListener);
    }
    
    public void UnsubscribeGenericEventHandler(string label) {
        if (genericEventHandlers==null) {
            return;
        }

        if (genericEventHandlers.ContainsKey(label)) {
            genericEventHandlers.Remove(label);
        }
    }
}
