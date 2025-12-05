using ECM2.Examples.FirstPerson;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobilWebGLPlayerRotator : MonoBehaviour {
    
    [SerializeField] private InputActionReference rotateAction;
    
    [SerializeField] private FirstPersonCharacter _character;
    
    [SerializeField] private bool invertYRotation = false;
    
    [SerializeField] private float rotationYSensibility = 25f;
    
    [SerializeField] private float rotationXSensibility = 25f;

    private void Start() {
        rotateAction.action.performed += OnRotatePerformed;
    }

    private void OnDestroy() {
        rotateAction.action.performed -= OnRotatePerformed;
    }

    private void OnRotatePerformed(InputAction.CallbackContext obj) {
        Vector2 rotateInputValue = obj.ReadValue<Vector2>();
        _character.AddControlPitchInput(invertYRotation ? -rotateInputValue.y * rotationYSensibility : rotateInputValue.y * rotationYSensibility);
        _character.AddControlYawInput(rotateInputValue.x * rotationXSensibility);
    }
}
