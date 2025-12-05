using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLauncher_BySpeed : MonoBehaviour
{
    [Header("Target and Launch Settings")]
    public Transform target;
    public float launchSpeed = 10f;

    [Header("Launch Control")]
    public bool launchOnStart = true;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (launchOnStart && target != null)
        {
            LaunchProjectile();
        }
    }

    public void LaunchProjectile()
    {
        if (target == null)
        {
            Debug.LogError("No target assigned for projectile launch.");
            return;
        }

        Vector3 velocity;
        bool success = CalculateLaunchVelocity(transform.position, target.position, launchSpeed, out velocity);

        if (success)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = true;

            rb.velocity = velocity;
        }
        else
        {
            Debug.LogWarning("Target is not reachable with the given speed.");
        }
    }

    /// <summary>
    /// Calculates the initial velocity needed to hit the target from a given point with a fixed speed.
    /// Returns false if the target is unreachable with the given speed.
    /// </summary>
    bool CalculateLaunchVelocity(Vector3 start, Vector3 end, float speed, out Vector3 velocity)
    {
        Vector3 toTarget = end - start;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;
        float gravity = Mathf.Abs(Physics.gravity.y);

        float speedSquared = speed * speed;
        float discriminant = speedSquared * speedSquared - gravity * (gravity * xz * xz + 2 * y * speedSquared);

        // Check if the target is reachable with this speed
        if (discriminant < 0)
        {
            velocity = Vector3.zero;
            return false;
        }

        float sqrtDiscriminant = Mathf.Sqrt(discriminant);

        // Use the lower angle (shorter arc)
        float time = Mathf.Sqrt((speedSquared - sqrtDiscriminant) / (gravity * gravity));

        // Calculate velocity components
        Vector3 velocityY = Vector3.up * (y / time + 0.5f * gravity * time);
        Vector3 velocityXZ = toTargetXZ / time;

        velocity = velocityXZ + velocityY;
        return true;
    }

    // Optional: Call this from another script or UI
    public void SetTargetAndLaunch(Transform newTarget)
    {
        target = newTarget;
        LaunchProjectile();
    }
}
