using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTester : MonoBehaviour {
    
    [SerializeField] private ObjectSpawner objectSpawner;

    [SerializeField] private List<GameObject> itemsToVisualize;

    [SerializeField] private KeyCode nextItemKey;

    [SerializeField] private int currentItemIndex;

    [SerializeField] private float nextItemCooldown;
    
    [SerializeField] private bool onShowNextItemCooldown;

    private void Start() {
        objectSpawner.SpawnOnRenderingLocation(itemsToVisualize[currentItemIndex]);
    }

    private void Update() {
        if (Input.GetKey(nextItemKey) && !onShowNextItemCooldown) {
            currentItemIndex = (currentItemIndex + 1) % itemsToVisualize.Count;
            objectSpawner.DestroySpawnedObject();
            objectSpawner.SpawnOnRenderingLocation(itemsToVisualize[currentItemIndex]);
            StartCoroutine(NextItemCooldown());
        }
    }

    private IEnumerator NextItemCooldown() {
        onShowNextItemCooldown = true;
        yield return new WaitForSeconds(nextItemCooldown);
        onShowNextItemCooldown = false;
    }
}
