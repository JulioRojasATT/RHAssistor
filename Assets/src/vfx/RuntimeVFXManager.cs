using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New VFX Manager", menuName = "GX/Managers/New VFX Manager")]
public class RuntimeVFXManager : NonSerializedScriptableValue<VFXManager> {
    public void PlayDefaultPSOnPosition(Transform target) {
        Value.PlayDefaultPSOnPosition(target);
    }
}
