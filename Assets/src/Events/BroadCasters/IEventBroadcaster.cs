using System;
using UnityEngine.Events;

public interface IEventBroadcaster<out T> {

    public void SubscribeGenericEventHandler(string label, EventHandler newListener);

    public void UnsubscribeGenericEventHandler(string label);
}
