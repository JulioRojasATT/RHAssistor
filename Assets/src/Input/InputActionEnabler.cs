using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionEnabler : MonoBehaviour {
    [SerializeField] private InputActionAsset inputActionAsset;

    private void Awake() {
        inputActionAsset.Enable();
    }
}