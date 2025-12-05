using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Effects/VFX", fileName = "New Instantiation Data")]
public class VFXInstantiationData : ScriptableObject {
    
    public int transformIndex;
    
    public float duration;
    
    public GameObject vfxPrefab;
}
