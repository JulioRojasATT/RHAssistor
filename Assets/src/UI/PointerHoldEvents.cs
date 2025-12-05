using UnityEngine;
using UnityEngine.Events;

public class PointerHoldEvents : MonoBehaviour
{

    [SerializeField] private UnityEvent onMouseDown;

    [SerializeField] private UnityEvent onMouseUp;        

    private void OnMouseDown()
    {
        onMouseDown.Invoke();
        
    }

    private void OnMouseUp()
    {
        onMouseUp.Invoke();
    }
}
