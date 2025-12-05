using System;
using UnityEngine;

public class ScriptableValue<T> : ScriptableObject{ 
    
    [SerializeField] protected T value;
    public T Value { get => value; }

    public EventHandler<ScriptableValueChangedEvent<T>> OnValueChangedEvent;

    public virtual void SetValue(T newValue) {
        OnValueChangedEvent?.Invoke(this,new ScriptableValueChangedEvent<T>(value, newValue));
        value = newValue;
    }
}


public class ScriptableValueChangedEvent<T> : EventArgs {
    
    public T oldValue;
    
    public T newValue;
    
    public ScriptableValueChangedEvent(T oldValue, T newValue) {
        this.oldValue = oldValue;
        this.newValue = newValue;
    }
}