using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Vector3", menuName = "Values/Vector3")]
public class Vector3ScriptableValue : ScriptableValue<Vector3> {
}


public class Vector3ValueChangedEventArgs : ScriptableValueChangedEvent<Vector3> {
    public Vector3ValueChangedEventArgs(Vector3 oldValue, Vector3 newValue) : base(oldValue, newValue) {
    }
}