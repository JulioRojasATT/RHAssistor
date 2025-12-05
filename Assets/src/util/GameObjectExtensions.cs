using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectExtensions : MonoBehaviour {

    [SerializeField] private UnityEvent OnEnableCallback;
    
    [SerializeField] private UnityEvent OnDisableCallback;
    
    [SerializeField] private UnityEvent OnStartCallback;
    
    [SerializeField] private UnityEvent OnAwakeCallback;

    [SerializeField] private UnityEvent OnDestroyCallback;

    [SerializeField] private UnityEvent OnFirstFrameCallback;

    private void Awake() {
        OnAwakeCallback.Invoke();
    }

    private void Start() {
        OnStartCallback.Invoke();
        StartCoroutine(WaitForFirstFrameAndExecuteEvents());
    }

    public void SelfDestroy() {
        Destroy(gameObject);
    }
    
    public void DestroyImmediateParent() {
        Destroy(transform.parent.gameObject);
    }

    private void OnEnable() {
        OnEnableCallback.Invoke();
    }

    private void OnDisable() {
        OnDisableCallback.Invoke();
    }

    private void OnDestroy()
    {
        OnDestroyCallback.Invoke();
    }

    public void DisableGameObject(){
        gameObject.SetActive(false);
    }

    public void DestroyComponent(Component component)
    {
        Destroy(component);
    }

    /// <summary>
    /// Toggles the gameObject to active or inactive
    /// </summary>
    public void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ClearParent() {
        transform.parent = null;
    }

    public IEnumerator WaitForFirstFrameAndExecuteEvents()
    {
        yield return new WaitForEndOfFrame();
        OnFirstFrameCallback.Invoke();
    }
}