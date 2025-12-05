using ECM2.Examples.FirstPerson;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualPlayerMovementControlConnector : MonoBehaviour
{
    [SerializeField] private ControlStick movementStick;

    [SerializeField] private FirstPersonCharacter _character;

    [Header("Input sensibility")]
    [SerializeField] private float movementSensibility = 0.5f;    

    private void Update()
    {
        // Moving the character
        Vector3 movementDirection = Vector3.zero;
        movementDirection += _character.GetRightVector() * movementStick.Horizontal * movementSensibility;
        movementDirection += _character.GetForwardVector() * movementStick.Vertical * movementSensibility;
        _character.SetMovementDirection(movementDirection);
    }
}
