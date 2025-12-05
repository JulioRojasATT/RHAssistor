using UnityEngine;

public class CharacterPathUpdater : MonoBehaviour
{
    [SerializeField] private EntityPathTracer pathTracer;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        if(!target) return;
        pathTracer.TracePathFromTo(transform.position, target.position, true);
    }

    public void UpdateTarget(Transform newTarget)
    {
        pathTracer.enabled = newTarget!=null;
        target = newTarget;
    }

    public void ClearTarget()
    {
        UpdateTarget(null);
        pathTracer.TracePathFromTo(Vector3.zero, Vector3.zero, true);
    }
}
