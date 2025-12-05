using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Vector2", menuName = "Values/Vector2")]
public class Vector2ScriptableValue : ScriptableValue<Vector2> {
}


public class Vector2ValueChangedEventArgs : ScriptableValueChangedEvent<Vector2> {
    public Vector2ValueChangedEventArgs(Vector2 oldValue, Vector2 newValue) : base(oldValue, newValue) {
    }
}