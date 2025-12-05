using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public abstract class GenericSelector<T> : MonoBehaviour where T : Interactable
{
    [SerializeField] protected Camera m_Camera;

    [SerializeField] protected BoolScriptableValue canInteract;

    [SerializeField] protected LayerMask interactableLayer;

    [SerializeField] protected List<T> selectedObjects;

    [SerializeField] protected float interactionRadius = 0.1f;

    [SerializeField] protected bool continuousDetection;
    
    [Header("Pointer tracking")]
    [SerializeField] private Vector3ScriptableValue pointerWorldPositionValue;

    [SerializeField] private FloatScriptableValue zCoordinates;

    [Header("Events")]
    [SerializeField] private UnityEvent onSelectionStarted;
    
    [SerializeField] private UnityEvent onSelectionEnded;

    [Header("Input")]
    [SerializeField] private InputActionReference selectionAction;

    [Header("Hover")]
    [SerializeField] protected BoolScriptableValue continuousHover;

    [SerializeField] private float hoverDistance;

    [SerializeField] private float hoverRadius;

    private void Start()
    {
        selectionAction.action.performed += OnSelectionPerformed;
        selectionAction.action.started += OnSelecionStarted;
        selectionAction.action.canceled += OnSelectionCanceled;
    }

    private void OnSelecionStarted(InputAction.CallbackContext obj)
    {
        if (!canInteract)
        {
            return;
        }
        StartSelection(Input.mousePosition);
    }

    private void OnSelectionCanceled(InputAction.CallbackContext obj)
    {
        if (!canInteract)
        {
            return;
        }
        StopSelection(Input.mousePosition);
    }

    private void OnSelectionPerformed(InputAction.CallbackContext obj) {
        if (!continuousDetection)
        {
            return;
        }
        TryInteractWithObjectsAtPosition(Input.mousePosition);
        //
        Vector3 pointerWorldPosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        pointerWorldPosition.z = zCoordinates.Value;
        pointerWorldPositionValue.SetValue(pointerWorldPosition);
    }

    private void Update()
    {
        if (!continuousHover.Value)
        {
            return;
        }
        if (!Physics.SphereCast(new Ray(m_Camera.transform.position, m_Camera.transform.forward), hoverRadius, out RaycastHit hit, hoverDistance, interactableLayer.value)) {
            return;
        }
        if(!hit.collider.TryGetComponent(out Interactable interactable)) {
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Input.mousePosition, interactionRadius);
    }

    public void Select(Interactable interactable) {
        interactable.OnSelected(interactable);
    }

    public virtual void StartSelection(Vector2 screenPosition) {
        onSelectionStarted?.Invoke();
    }

    public virtual void StopSelection(Vector2 screenPosition) {
        onSelectionEnded?.Invoke();
    }

    public abstract void TryInteractWithObjectsAtPosition(Vector2 screenPosition);
}
