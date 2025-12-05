using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Runtime GameObject Reference", menuName = "Values/Runtime GameObject")]
public class RuntimeGameObjectScriptableValue : NonSerializedScriptableValue<GameObject> {

    public T GetComponent<T>() {
        if (value) {
            return value.GetComponent<T>(); 
        }
        object nullValue = null;
        return (T) nullValue;
    }
}

public class RuntimeGameObjectValueChangedEventArgs : ScriptableValueChangedEvent<GameObject> {
    public RuntimeGameObjectValueChangedEventArgs(GameObject oldValue, GameObject newValue) : base(oldValue, newValue) {
    }
}
