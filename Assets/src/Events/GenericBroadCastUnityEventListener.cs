using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBroadCastUnityEventListener<T> : MonoBehaviour where T: EventArgs {
    
    public List<EventBroadCaster<T>> eventBroadCasters;
}
