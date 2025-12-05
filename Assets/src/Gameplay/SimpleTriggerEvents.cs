using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTriggerEvents : MonoBehaviour {
    [SerializeField] private UnityEvent onEnterEvent;
    
    [SerializeField] private UnityEvent onExitEvent;

    [SerializeField] private List<string> tagsToCheck;
    
    private void OnTriggerEnter(Collider other) {
        /*if (!tagsToCheck.Contains(other.tag)) {
            return;
        }*/
        onEnterEvent?.Invoke();
    }
    
    private void OnTriggerExit(Collider other) {
        /*if (!tagsToCheck.Contains(other.tag)) {
            return;
        }*/
        onExitEvent?.Invoke();
    }
}
