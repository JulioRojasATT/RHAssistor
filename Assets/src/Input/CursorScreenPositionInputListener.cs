using UnityEngine;

public class CursorScreenPositionInputListener : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private ObjectRotator objectRotator;
    
    private float _deltaX;

    private float _deltaY;

    private Vector2 _previousCursorPosition;

    private bool _firstTimeTouchingScreen = true;
    
    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            _firstTimeTouchingScreen = true;
            return;
        }
        if (_firstTimeTouchingScreen) {
            _firstTimeTouchingScreen = false;
            _previousCursorPosition = Input.mousePosition;
        }
        _deltaX = _previousCursorPosition.x - Input.mousePosition.x;
        _deltaY = _previousCursorPosition.y - Input.mousePosition.y;
        objectRotator.RotateUp(_deltaX);
        objectRotator.RotateRight(_deltaY);
        _previousCursorPosition = Input.mousePosition;
    }
}
