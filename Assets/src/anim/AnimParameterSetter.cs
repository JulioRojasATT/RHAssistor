using UnityEngine;

public class AnimParameterSetter : MonoBehaviour {
    [SerializeField] private string parameterName;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetParameterIntValue(int value) {
        _animator.SetInteger(parameterName, value);
    }

    public void SetParameterFloatValue(int value)
    {
        _animator.SetFloat(parameterName, value);
    }
}
