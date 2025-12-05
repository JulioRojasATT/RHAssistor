using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputSystemTester : MonoBehaviour
{
    [SerializeField] private UnityEvent onActivationEvent;

    [SerializeField] private InputActionReference activationAction;

    private void Awake()
    {
        activationAction.action.Enable();
        activationAction.action.performed += OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext obj)
    {
        onActivationEvent.Invoke();
    }
}
