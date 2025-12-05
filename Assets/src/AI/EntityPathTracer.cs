using UnityEngine;
using UnityEngine.AI;

public class EntityPathTracer : MonoBehaviour
{
    [SerializeField] private LineRenderer pathRenderer;

    [SerializeField] private Vector3 pathOffset;

    [SerializeField] private float updateRefreshRate;
    public float UpdateRefreshRate => updateRefreshRate;

    [SerializeField] private Transform target;

    private NavMeshPath path;

    private void Update(){
        if (!target){
            return;
        }
        TracePathFromTo(transform.position, target.position, true);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    public void ClearTarget() {
        target = null;
        pathRenderer.positionCount= 0;
        pathRenderer.SetPositions(new Vector3[] { });
    }

    public void TracePathFromTo(Vector3 startPosition, Vector3 endPosition, bool reverted = false) {
        path = new NavMeshPath();
        if (reverted)
        {
            NavMesh.CalculatePath(endPosition, startPosition, NavMesh.AllAreas, path);
        } else
        {
            NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path);            
        }
        Vector3[] points = path.corners;
        for (int i = 0;i < points.Length; i++) {
            points[i] += pathOffset;
        }
        pathRenderer.positionCount = points.Length;
        pathRenderer.SetPositions(points);
    }
}
