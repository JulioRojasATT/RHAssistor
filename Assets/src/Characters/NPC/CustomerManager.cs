using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {
    
    [Header("Customer control")]
    [SerializeField] private List<GeneralHumanBehaviours> customerPrefabs;

    [SerializeField] private PathManager pathManager;
    
    [Header("Spawn control")]
    [SerializeField] private bool spawnCustomers;

    [SerializeField] private List<Transform> spawnLocations;

    private const string CUSTOMER_PATH_GROUPS_ID = "Customer";

    private int _generatedCustomers = 0;
    
    /// <summary>
    /// Spawns customers each 
    /// </summary>
    public void SpawnCustomersRepeat(float seconds) {
        StartCoroutine(SpawnCustomers(new WaitForSeconds(seconds)));
    }

    public void SpawnRandomCustomerAtTransform(Transform spawnPoint)
    {
        if (customerPrefabs.Count <= 0)
        {
            return;
        }
        GeneralHumanBehaviours newCustomer =
            Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPoint.position, spawnPoint.rotation);
        newCustomer.gameObject.name = "Customer#" + _generatedCustomers++; 
        newCustomer.FollowPath(pathManager.GetRandomPathFromGroup(CUSTOMER_PATH_GROUPS_ID));
        StartCoroutine(DeleteCustomerOnStopped(newCustomer));
    }

    public IEnumerator SpawnCustomers(YieldInstruction waitFunc) {
        while (spawnCustomers) {
            SpawnRandomCustomerAtRandomSpawnLocation();
            yield return waitFunc;
        }
    }
    
    public void SpawnRandomCustomerAtRandomSpawnLocation() {
        SpawnRandomCustomerAtTransform(spawnLocations[Random.Range(0, spawnLocations.Count)]);
    }

    public IEnumerator DeleteCustomerOnStopped(GeneralHumanBehaviours entity) {
        yield return new WaitUntil(() => entity.Stopped);
        Destroy(entity.gameObject);        
    }
}
