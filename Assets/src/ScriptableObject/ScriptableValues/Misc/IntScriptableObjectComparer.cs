using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class IntScriptableObjectComparer : MonoBehaviour
{
    [SerializeField] private IntScriptableValue value;

    [SerializeField] private UnityEvent onComparisonTrue;

    [SerializeField] private UnityEvent onComparisonFalse;

    [SerializeField] private List<EventByValue> valueEventDuples;

    private Dictionary<int, UnityEvent> eventsByValue = new Dictionary<int, UnityEvent>();

    private void Awake()
    {
        valueEventDuples.ForEach(x => eventsByValue.Add(x.value,x.onEqualEvent));
    }
     
    public void IsEqualThan(int valueToCompare)
    {
        if (valueToCompare.Equals(value.Value))
        {
            onComparisonTrue.Invoke();
            return;
        }
        onComparisonFalse.Invoke();
        return;
    }

    /// <summary>
    /// Checks the value and executes the event associated with that id (If it exists)
    /// </summary>
    public void CheckEventsByValue() {
        if (eventsByValue.ContainsKey(value.Value)) {
            eventsByValue[value.Value].Invoke();
        }
    }

    public void IsEqualThan(IntScriptableValue valueToCompare)
    {
        IsEqualThan(valueToCompare.Value);
    }

    public void IsGreaterOrEqualThan(int valueToCompare)
    {
        if (valueToCompare.CompareTo(value.Value) >= 0)
        {
            onComparisonTrue.Invoke();
            return;
        }
        onComparisonFalse.Invoke();
        return;
    }

    public void IsGreaterOrEqualThan(IntScriptableValue valueToCompare)
    {
        IsGreaterOrEqualThan(valueToCompare.Value);
    }

    public void IsGreaterThan(int valueToCompare)
    {
        if (valueToCompare.CompareTo(value.Value) > 0)
        {
            onComparisonTrue.Invoke();
            return;
        }
        onComparisonFalse.Invoke();
        return;
    }

    public void IsGreaterThan(IntScriptableValue valueToCompare)
    {
        IsGreaterThan(valueToCompare.Value);
    }
}
