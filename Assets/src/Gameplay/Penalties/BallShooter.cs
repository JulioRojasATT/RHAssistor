using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidBody;

    [SerializeField] private float horizontalKickForce;
    
    [SerializeField] private float verticalKickForce;

    public void ShootTowardsTarget(Transform target) {
        Vector3 kickForce = Vector3.Normalize(target.transform.position - ballRigidBody.transform.position);
        kickForce.Scale(new Vector3(horizontalKickForce, verticalKickForce,horizontalKickForce));
        ballRigidBody.AddForce(kickForce, ForceMode.Impulse);
    }
}
