using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalizedEvent : MonoBehaviour {
    [SerializeField] private IntScriptableValue languageValue;

    [SerializeField] private UnityEvent[] events;

    public void Invoke() {
        events[languageValue.Value].Invoke();
    }
}
