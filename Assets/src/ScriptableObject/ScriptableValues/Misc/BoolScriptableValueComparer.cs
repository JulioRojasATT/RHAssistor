using UnityEngine;
using UnityEngine.Events;

public class BoolScriptableValueComparer : MonoBehaviour
{
    [SerializeField] private UnityEvent onComparisonTrue;

    [SerializeField] private UnityEvent onComparisonFalse;

    public void IsTrue(BoolScriptableValue valueToCompare) => IsTrue(valueToCompare.Value);

    public void IsFalse(BoolScriptableValue valueToCompare) => IsFalse(valueToCompare.Value);
    
    public void IsTrue(bool valueToCompare)
    {
        if (valueToCompare)
        {
            onComparisonTrue.Invoke();
            return;
        }
        onComparisonFalse.Invoke();
    }

    public void IsFalse(bool valueToCompare)
    {
        if (!valueToCompare)
        {
            onComparisonTrue.Invoke();
            return;
        }
        onComparisonFalse.Invoke();
    }
}
