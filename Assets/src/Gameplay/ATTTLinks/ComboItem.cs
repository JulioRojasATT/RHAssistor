using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboItem : MonoBehaviour
{

    [SerializeField] private UnityEvent onReveal;

    [SerializeField] private UnityEvent onHide;

    public void Reveal()
    {
        onReveal?.Invoke();
    }

    public void Hide()
    {
        onHide?.Invoke();
    }
}
