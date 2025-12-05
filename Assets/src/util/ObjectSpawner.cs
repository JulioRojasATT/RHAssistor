using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour{

    public MonoBehaviourGeneralInfo spawnedObject;

    public Transform objectRenderingLocation;

    [SerializeField] private UnityEvent<MonoBehaviourGeneralInfo> onSpawnedObject;

    public void DestroySpawnedObject() {
        if (spawnedObject){
            Destroy(spawnedObject.gameObject);
        }
        
    }

    public void SpawnOnRenderingLocation(GameObject objectToSpawn)
    {
        spawnedObject = Instantiate(objectToSpawn, objectRenderingLocation.transform).GetComponent<MonoBehaviourGeneralInfo>();
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.transform.localRotation = Quaternion.identity;
        onSpawnedObject.Invoke(spawnedObject);
    }
}
