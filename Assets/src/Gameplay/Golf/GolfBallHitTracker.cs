using UnityEngine;

public class GolfBallHitTracker : MonoBehaviour
{
    public int hitCount = 0;
    private Rigidbody rb;
    private float velocityThreshold = 1.0f; // Minimum force to count as a hit
    private bool isHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Ball has stopped moving ? reset hit detection
        if (rb.velocity.magnitude < 0.05f && isHit)
        {
            isHit = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isHit && rb.velocity.magnitude > velocityThreshold && collision.relativeVelocity.magnitude > velocityThreshold)
        {
            hitCount++;
            isHit = true;
            Debug.Log("Hit Count: " + hitCount);
        }
    }
}
