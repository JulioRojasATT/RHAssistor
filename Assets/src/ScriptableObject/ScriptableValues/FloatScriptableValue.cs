using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Scriptable Float", menuName = "Values/Float")]
public class FloatScriptableValue : ScriptableValue<float> {

    /// <summary>
    /// Sets the value and invokes the callback. Used via a slider to simplify things
    /// </summary>
    /// <param name="newValue"></param>
    public virtual void SetValue(Slider slider) {
        OnValueChangedEvent?.Invoke(this,new FloatValueChangedEventArgs(value, slider.value));
        value = slider.value;
    }
}


public class FloatValueChangedEventArgs : ScriptableValueChangedEvent<float> {
    public FloatValueChangedEventArgs(float oldValue, float newValue) : base(oldValue, newValue) {
    }
}