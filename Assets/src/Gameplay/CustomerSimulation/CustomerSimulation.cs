using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerSimulation : MonoBehaviour {
    [Header("Spawn settings")]
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Transform> exits;
    [SerializeField] private List<Transform> queuePositions;
    [SerializeField] private int maxCustomers;
    [SerializeField] private UnityEvent onSimulationStarted;    
    [SerializeField] private UnityEvent onSimulationEnded;
    
    [Header("External Components")]

    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private ScoreTester scoreTester;

    private List<GeneralHumanBehaviours> _spawnedCustomers = new List<GeneralHumanBehaviours>();

    private GeneralHumanBehaviours _currentCustomer;

    private const float SECONDS_BETWEEN_CUSTOMER_GENERATIONS = 1f;

    private bool _inSimulation;

    public void TryStartCustomerServingSimulation()
    {
        if(!_inSimulation) {
            StartCoroutine(CustomerServingSimulation());
        }        
    }

    public IEnumerator CustomerServingSimulation()
    {
        _inSimulation = true;
        onSimulationStarted?.Invoke();
        for (int i = 0; i < customers.Count; i++) {
            GeneralHumanBehaviours newCustomer = Instantiate(customers[i], spawnPoint.position, spawnPoint.rotation).GetComponent<GeneralHumanBehaviours>();
            _spawnedCustomers.Add(newCustomer);
            newCustomer.GoTo(queuePositions[i]);
            yield return new WaitForSeconds(SECONDS_BETWEEN_CUSTOMER_GENERATIONS);
        }        
        while (_spawnedCustomers.Count>0) {
            _currentCustomer = _spawnedCustomers[0];
            _spawnedCustomers.RemoveAt(0);
            // Wait for the customer to arrive to its queue position
            yield return new WaitUntil(()=>_currentCustomer.HasArrivedToLastLocation);
            ConversatingNPC talkingCharacter = _currentCustomer.GetComponent<ConversatingNPC>();
            talkingCharacter.Conversations.ForEach(conversation => dialogueManager.AddConversation(conversation));
            dialogueManager.StartConversation(talkingCharacter.DefaultConversationName);
            yield return new WaitWhile(() => dialogueManager.InConversation);
            scoreTester.IncreaseScore(dialogueManager.CurrentConversationScore);
            // Wait for it to leave the store.
            _currentCustomer.GoToAndDestroy(exits[0]);
            talkingCharacter.Conversations.ForEach(conversation => dialogueManager.RemoveConversation(conversation));
            for (int i = 0; i < _spawnedCustomers.Count; i++)
            {
                _spawnedCustomers[i].GoTo(queuePositions[i]);
            }
        }
        onSimulationEnded?.Invoke();
        _inSimulation = false;
    }
}