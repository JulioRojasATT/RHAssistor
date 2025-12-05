using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GenericTimerEvent : MonoBehaviour {

    [SerializeField] private UnityEvent onTimerCompletedEvent;

    public void Invoke(float seconds)
    {
        StartCoroutine(StartTimerCor(seconds));
    }

    public IEnumerator StartTimerCor(float seconds) {
        yield return new WaitForSeconds(seconds);
        onTimerCompletedEvent.Invoke();
    }
}
