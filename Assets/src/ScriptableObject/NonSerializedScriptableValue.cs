using System;
using UnityEngine;

public class NonSerializedScriptableValue<T> : ScriptableObject{ 
    
    protected T value;
    public T Value { get => value; }

    public EventHandler<ScriptableValueChangedEvent<T>> OnValueChangedEvent;

    public virtual void SetValue(T newValue) {
        OnValueChangedEvent?.Invoke(this,new ScriptableValueChangedEvent<T>(value, newValue));
        value = newValue;
    }
}