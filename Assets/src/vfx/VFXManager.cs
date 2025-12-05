using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for a list of transforms that weapons can use to instantiate effects on it
/// </summary>
public class VFXManager : MonoBehaviour {

    public List<Transform> instanceTransforms;

    public ParticleSystem defaultParticleSystem;

    public void PlayDefaultPSOnPosition(Transform target) {
        defaultParticleSystem.transform.position = target.position;
        defaultParticleSystem.Play(true);
    }

    public GameObject InstantiateVFXInTransform(int transformIndex, float duration, GameObject vfxPrefab) {
        GameObject vfxInstance = Instantiate(vfxPrefab, instanceTransforms[transformIndex].position, instanceTransforms[transformIndex].rotation);
        Destroy(vfxInstance, duration);
        return vfxInstance;
    }
    
    public GameObject InstantiateVFXInTransform(int transformIndex, float duration, GameObject vfxPrefab, bool parentToTransform) {
        GameObject vfxInstance = InstantiateVFXInTransform(transformIndex, duration, vfxPrefab);
        if (parentToTransform) {
            vfxInstance.transform.parent = instanceTransforms[transformIndex];
        }
        return vfxInstance;
    }
    
    public void InstantiateVFXUsingData(VFXInstantiationData instantiationData) {
        GameObject vfxInstance = Instantiate(instantiationData.vfxPrefab, instanceTransforms[instantiationData.transformIndex].position,
            instanceTransforms[instantiationData.transformIndex].rotation);
        Destroy(vfxInstance, instantiationData.duration);
    }
}
