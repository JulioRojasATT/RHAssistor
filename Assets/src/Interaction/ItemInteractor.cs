using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ItemInteractor : MonoBehaviour
{
    [SerializeField] protected Camera m_Camera;

    [SerializeField] private BoolScriptableValue canInteract;
    public bool CanInteract {
        get => canInteract.Value;
        set => canInteract.SetValue(value);
    }

    [SerializeField] private LayerMask interactableLayer;

    [SerializeField] protected float maxRaycastDistance = 10f;

    [SerializeField] private float sphereRadius = 0.5f;

    [SerializeField] private BoolScriptableValue isMobileWebGL;

    protected Interactable currentlyHoveredObject;

    protected Interactable newHoveredObject;

    protected Interactable lastSelectedObject;

    [Header("Events")]
    [SerializeField] private UnityEvent<Interactable> onSelected;

    [SerializeField] private UnityEvent<Interactable> onUnSelected;

    [Header("Input")]
    [SerializeField] private InputActionReference selectAction;

    private void Start()
    {
        selectAction.action.started += OnSelectStarted;
        selectAction.action.canceled += OnSelectCanceled;
    }

    private void OnSelectCanceled(InputAction.CallbackContext obj)
    {
        if (!lastSelectedObject || !canInteract.Value)
        {            
            return;
        }
        onUnSelected?.Invoke(lastSelectedObject);
        lastSelectedObject.OnUnSelected(lastSelectedObject);
        lastSelectedObject = null;
    }

    private void OnSelectStarted(InputAction.CallbackContext obj)
    {
        if (!canInteract.Value)
        {
            return;
        }
        if (TryInteractWithScreenRaycast(out Interactable detectedObject))
        {
            lastSelectedObject = detectedObject;
        }
    }

    public void ClearHoverableData() {
        if (newHoveredObject)
        {
            newHoveredObject.OnHoverExited(newHoveredObject);
            newHoveredObject = null;
        }
        if (!currentlyHoveredObject) {
            return;
        }
        currentlyHoveredObject.OnHoverExited(currentlyHoveredObject);
        currentlyHoveredObject = null;        
    }

    public bool TryInteractWithScreenRaycast(out Interactable interactedObject) {
        interactedObject = null;
        if (!Physics.SphereCast(new Ray(m_Camera.transform.position, m_Camera.transform.forward), sphereRadius,
            out RaycastHit hit, maxRaycastDistance, interactableLayer.value))
        {
            return false;
        }        
        if (!hit.collider || !hit.collider.TryGetComponent(out interactedObject))
        {
            return false;
        }
        onSelected?.Invoke(interactedObject);
        SelectObject(currentlyHoveredObject);        
        return true;
    }

    public void SelectObject(Interactable interactable)
    {
        lastSelectedObject = interactable;
        lastSelectedObject.OnSelected(interactable);
    }

    protected virtual void FixedUpdate()
    {
        if (!canInteract.Value) {
            return;
        }
        if(!Physics.SphereCast(new Ray(m_Camera.transform.position, m_Camera.transform.forward), sphereRadius, out RaycastHit hit, maxRaycastDistance, interactableLayer.value)) {
            newHoveredObject= null;
        }
        if (!hit.collider || !hit.collider.TryGetComponent(out newHoveredObject))
        {
        }        

        if (currentlyHoveredObject == newHoveredObject)
        {
            return;
        }
        if (newHoveredObject != null)
        {
            newHoveredObject.OnHoverEntered(newHoveredObject);
        }

        if (currentlyHoveredObject != null && currentlyHoveredObject.isActiveAndEnabled)
        {
            currentlyHoveredObject.OnHoverExited(newHoveredObject);
        }
        currentlyHoveredObject = newHoveredObject;
    }
}
