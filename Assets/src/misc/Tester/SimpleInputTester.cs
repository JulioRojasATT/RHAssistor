using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleInputTester : MonoBehaviour
{
    [SerializeField] private KeyCode activationKeyCode;

    [SerializeField] private UnityEvent onActivationEvent;

    void Update()
    {
        if(Input.GetKeyDown(activationKeyCode)) {
            onActivationEvent.Invoke();
        }
        
    }
}
