using UnityEngine;

public class UIDisplayToggler : MonoBehaviour {

    private Animator _animator;

    private static int VISIBLE_TRIGGER_HASH = Animator.StringToHash("Visible");
    
    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void ToggleVisible() {
        _animator.SetBool(VISIBLE_TRIGGER_HASH, !_animator.GetBool(VISIBLE_TRIGGER_HASH));
    }

    public void SetVisible(bool visible)
    {
        _animator.SetBool(VISIBLE_TRIGGER_HASH, visible);
    }
}
