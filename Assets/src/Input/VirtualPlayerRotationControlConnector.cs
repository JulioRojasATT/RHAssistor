using ECM2.Examples.FirstPerson;
using UnityEngine;

public class VirtualPlayerRotationControlConnector : MonoBehaviour
{
    [SerializeField] private ControlStick rotationStick;

    [SerializeField] private FirstPersonCharacter _character;

    [SerializeField] private SimpleCameraRotator _simpleCameraRotator;

    [Header("Rotation Parameters")]
    [SerializeField] private bool invertYRotation = false;

    [SerializeField] private float rotationYSensibility = 25f;

    [SerializeField] private float rotationXSensibility = 25f;

    private void Update()
    {
        if (!_character) {
            return;
        }
        _character.AddControlPitchInput(invertYRotation ? -rotationStick.Vertical * rotationYSensibility : rotationStick.Vertical * rotationYSensibility);
        _character.AddControlYawInput(rotationStick.Horizontal * rotationXSensibility);
        if (!_simpleCameraRotator) {
            return;
        }
        _simpleCameraRotator.deltaX = rotationStick.Horizontal * rotationXSensibility;
        _simpleCameraRotator.deltaY = invertYRotation ? -rotationStick.Vertical * rotationYSensibility : rotationStick.Vertical * rotationYSensibility;
    }
}
