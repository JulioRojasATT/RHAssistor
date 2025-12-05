using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimScoreCalculator : MonoBehaviour
{
    public UnityEvent kickCorrectEvent;

    public UnityEvent kickIncorrectEvent;

    public UnityEvent kickCenterEvent;

    [Header("Kick Event")]
    [SerializeField] private IntScriptableValue kickAnswerValue;

    public void CalculateScoreAndCallEvents()
    {
        switch (kickAnswerValue.Value)
        {
            case 0:
                Debug.Log("Emitting kick correct event");
                kickCorrectEvent.Invoke();
                break;
            case 1:
                Debug.Log("Emitting kick incorrect event");
                kickIncorrectEvent.Invoke();
                break;
            case 2:
                Debug.Log("Emitting kick center event");
                kickCenterEvent.Invoke();
                break;
        }
    }
}
