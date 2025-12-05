using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnPerformedInputActionEvents : MonoBehaviour {

    [SerializeField] private InputActionReference[] actionReferences;

    [SerializeField] private UnityEvent[] events;

    private void Awake()
    {
        for(int i=0; i<actionReferences.Length; i++)
        {
            actionReferences[i].action.Enable();
            int untrappedVariable = i;
            actionReferences[i].action.performed += x=>ProcessInput(x, untrappedVariable);
        }
    }

    public void ProcessInput(InputAction.CallbackContext callbackContext, int eventIndex) {
        events[eventIndex].Invoke();
    }
}