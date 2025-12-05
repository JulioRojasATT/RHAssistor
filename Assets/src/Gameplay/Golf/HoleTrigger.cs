using UnityEngine;
using UnityEngine.Events;

public class HoleTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onHoled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolfBall"))
        {
            onHoled.Invoke();
        }
    }
}
