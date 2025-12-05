using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Bool", menuName = "Values/Bool")]
public class BoolScriptableValue : ScriptableValue<bool> {

    public void Toggle() {
        value = !value;
    }
}
