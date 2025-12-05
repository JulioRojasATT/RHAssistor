using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLauncher : MonoBehaviour
{
    [Header("Target and Launch Settings")]
    public float desiredApexHeight = 5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDesiredApexHeight(float height) {
        desiredApexHeight = height;
    }

    public void LaunchProjectile(Transform target)
    {

        Vector3 start = transform.position;
        Vector3 end = target.position;

        // Calculate initial velocity
        Vector3 velocity = CalculateLaunchVelocity(start, end, desiredApexHeight);

        // Reset velocity and apply the new one
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;

        rb.velocity = velocity;
    }

    Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float apexHeight)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 displacementXZ = new Vector3(end.x - start.x, 0, end.z - start.z);
        float horizontalDistance = displacementXZ.magnitude;

        float heightDifference = end.y - start.y;
        float heightToApex = apexHeight - start.y;

        // Vertical velocity to reach apex
        float verticalVelocity = Mathf.Sqrt(2 * gravity * heightToApex);

        // Time to reach apex
        float timeToApex = verticalVelocity / gravity;

        // Total vertical distance from apex to target.y
        float fallDistance = apexHeight - end.y;
        float timeFromApexToTarget = Mathf.Sqrt(2 * fallDistance / gravity);

        float totalTime = timeToApex + timeFromApexToTarget;

        Vector3 horizontalVelocity = displacementXZ / totalTime;

        return new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z);
    }
}
