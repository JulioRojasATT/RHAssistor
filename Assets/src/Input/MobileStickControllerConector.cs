using ECM2.Examples.FirstPerson;
using System.Collections;
using UnityEngine;

public class MobileStickControllerConector : MonoBehaviour
{
    [SerializeField] private bl_Joystick moveJoystick;

    [SerializeField] private FirstPersonCharacter _character;

    [SerializeField] private BoolScriptableValue isMobileWebGL;

    [Header("Input sensibility")]
    [SerializeField] private float movementSensibility = 0.5f;

    private void Update()
    {
        // Moving the character
        Vector3 movementDirection = Vector3.zero;
        movementDirection += _character.GetRightVector() * moveJoystick.Horizontal * movementSensibility;
        movementDirection += _character.GetForwardVector() * moveJoystick.Vertical * movementSensibility;
        _character.SetMovementDirection(movementDirection);
    }
}
