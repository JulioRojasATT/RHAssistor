using System.Collections.Generic;
using UnityEngine;

public class ComboSelector : GenericSelector<ComboComponent>
{
    [SerializeField] private LineRendererObjectPool lineRendererPool;
    
    [SerializeField] private LineRenderer lastRenderer;

    [SerializeField] private FloatScriptableValue zCoordinate;
    private float ZCoordinate => zCoordinate.Value;

    [SerializeField] private ComboManager comboManager;

    private HashSet<int> addedRows = new HashSet<int>();

    private void Awake() {
        lineRendererPool.CreateInitialInstances();
    }

    public override void StopSelection(Vector2 screenPosition){
        base.StopSelection(screenPosition);
        if (selectedObjects.Count < 0) {
            return;
        }
        lineRendererPool.ForEachOccupied(x=>x.positionCount = 0);
        lineRendererPool.ReturnAllToPool();
        lastRenderer = null;
        comboManager.CheckCurrentComboIsDetected(selectedObjects.ConvertAll<string>(x => x.ID).ToArray());
        selectedObjects.ForEach(x => x.OnUnSelected(x));
        selectedObjects.Clear();
        addedRows.Clear();
    }
    
    public override void TryInteractWithObjectsAtPosition(Vector2 screenPosition) {
        // Update raycast
        Vector3 worldMousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        if (selectedObjects.Count > 0) {
            lastRenderer.SetPosition(0, selectedObjects[^1].transform.position);
            lastRenderer.SetPosition(1, new Vector3(worldMousePosition.x, worldMousePosition.y, ZCoordinate));
        }
        
        RaycastHit2D hit = Physics2D.CircleCast(m_Camera.ScreenToWorldPoint(screenPosition), interactionRadius, Vector2.zero);
        if (!hit || !hit.collider || !hit.collider.TryGetComponent(out ComboComponent interactedObject) || addedRows.Contains(interactedObject.Row))
        {
            return;
        }
        addedRows.Add(interactedObject.Row);
        if (lastRenderer != null) {
            lastRenderer.SetPosition(0, selectedObjects[^1].transform.position);
            lastRenderer.SetPosition(1, interactedObject.transform.position);
        }
        lastRenderer = lineRendererPool.OccupyOne(out bool createdNewInstance);        
        lastRenderer.positionCount = 2;
        lastRenderer.SetPosition(0, interactedObject.transform.position);
        lastRenderer.SetPosition(1, worldMousePosition);
        selectedObjects.Add(interactedObject);
        Select(interactedObject);
    }
}
