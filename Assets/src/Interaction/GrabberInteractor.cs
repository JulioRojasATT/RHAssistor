using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberInteractor : ItemInteractor {

    [SerializeField] private LayerMask hoverableLayer;

    [SerializeField] private float maxSelectionDistance;

    [SerializeField] private Transform tracker;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!CanInteract) {
            return;
        }
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxSelectionDistance, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit hit, maxSelectionDistance, hoverableLayer.value)) {
            tracker.position = hit.point;
        }
    }

    public void ParentSelectedObjectToTracker(Interactable interactable) {
        if (interactable) {
            interactable.transform.parent = tracker;
        }
    }

    public void UnparentSelectedObjectFromTracker(Interactable interactable)
    {
        if (interactable) {
            interactable.transform.parent = null;
        }
    }
}
