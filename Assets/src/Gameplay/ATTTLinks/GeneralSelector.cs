using System.Collections.Generic;
using UnityEngine;

public class GeneralSelector : GenericSelector<ComboComponent>
{

    

    public override void StopSelection(Vector2 screenPosition){
        base.StopSelection(screenPosition);
        if (selectedObjects.Count < 0) {
            return;
        }
        selectedObjects.ForEach(x => x.OnUnSelected(x));
        selectedObjects.Clear();
    }
    
    public override void TryInteractWithObjectsAtPosition(Vector2 screenPosition) {
        // Update raycast
        Vector3 worldMousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.CircleCast(m_Camera.ScreenToWorldPoint(screenPosition), interactionRadius, Vector2.zero);
        if (!hit || !hit.collider || !hit.collider.TryGetComponent(out ComboComponent interactedObject))
        {
            return;
        }
        selectedObjects.Add(interactedObject);
        Select(interactedObject);
    }
}
