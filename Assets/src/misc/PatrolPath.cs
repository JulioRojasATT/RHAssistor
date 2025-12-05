using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [Tooltip("List of waypoints the entity will patrol in order.")]
    public Transform[] waypoints;

    [Tooltip("Speed at which the entity moves.")]
    public float moveSpeed = 3f;

    [Tooltip("Distance threshold to determine if a waypoint is reached.")]
    public float waypointTolerance = 0.2f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Move toward the current waypoint
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optional: Rotate toward target
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }

        // Check if the waypoint is reached
        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointTolerance)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 current = waypoints[i].position;
            Vector3 next = waypoints[(i + 1) % waypoints.Length].position;
            Gizmos.DrawLine(current, next);
            Gizmos.DrawSphere(current, 0.2f);
        }
    }
}
