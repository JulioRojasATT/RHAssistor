using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GeneralHumanBehaviours : MonoBehaviour {

    [Header("Agent control")]
    [SerializeField] private bool stopped;
    public bool Stopped => stopped;

    [SerializeField] private float walkSpeed = 1f;
    
    [SerializeField] private float runSpeed = 3f;
    
    [SerializeField] private bool autoPatrolAtStart;

    [SerializeField] private EntityPath autoPatrolPath;

    /// <summary>
    /// How much extra time we allow an agent to take to arrive to it's target before cancelling it's GoTo movement.
    /// </summary>
    [SerializeField] private float pathTravelCancellationTimeTolerance = 5f;

    [SerializeField] private float pathTravelCancellationTimeTolerancePerCorner = 0.5f;
    
    [Header("External Components")]
    [SerializeField] private AnimationBridge m_animator;

    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private BehaviourInterpreter behaviourInterpreter;

    [Header("Movement info")]
    [SerializeField] private int currentTargetIndex = 0;

    [SerializeField] private Transform currentTarget;

    [Header("Rotation control")]
    [SerializeField] float RotationSpeed = 5f;

    private const float FOLLOW_UPDATE_RATE = 0.1f;

    private bool _hasArrivedToLastLocation= false;
    public bool HasArrivedToLastLocation => _hasArrivedToLastLocation;

    private void Start() {
        if (autoPatrolAtStart) {
            Patrol(autoPatrolPath);
        }
    }

    public void PlayBehaviourByName(string behaviourName) {
        StartCoroutine(PlayBehaviourCoroutine(behaviourInterpreter.GetBehaviourByName(behaviourName)));
    }
    
    public IEnumerator PlayBehaviourCoroutine(ChainedBehaviour behaviour) {
        for (var i = 0; i < behaviour.functionList.Count; i++) {
            yield return behaviour.functionList[i].Invoke(this, behaviour.argumentsList[i]);
        }
    }

    public void Run() {
        m_animator.SetMovementSpeed(runSpeed);
        navMeshAgent.speed = runSpeed;
    }
    
    public void Walk() {
        m_animator.SetMovementSpeed(walkSpeed);
        navMeshAgent.speed = walkSpeed;
    }

    public void StopPatrol()
    {
        StopAllCoroutines();
        navMeshAgent.isStopped = true;
        m_animator.StopWalking();
        m_animator.StopTurningRight();
    }

    public void Patrol(EntityPath path) {
        StartCoroutine(FollowPath(path, true));
    }

    public void Follow(Transform target)
    {
        StartCoroutine(FollowCor(target));
    }

    public IEnumerator FollowCor(Transform target, bool stopWhenTouchedFirstTime = false) {
        stopped = false;
        navMeshAgent.isStopped = false;
        while (!stopped) {
            if(Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance) {
                m_animator.StopWalking();
                if (stopWhenTouchedFirstTime) {
                    stopped = true;
                    break;
                }
            } else
            {
                m_animator.StartWalking();
            }
            navMeshAgent.SetDestination(target.position);
            yield return new WaitForSeconds(FOLLOW_UPDATE_RATE);
        }
    }

    public void FollowPath(EntityPath path)
    {
        StartCoroutine(FollowPath(path, false));
    }

    public IEnumerator FollowPath(EntityPath path, bool repeatRoute = false)
    {
        stopped = false;
        currentTargetIndex = 0;
        navMeshAgent.isStopped = false;
        while (currentTargetIndex < path.PathCheckpoints.Count) {
            currentTarget = path.PathCheckpoints[currentTargetIndex];
            yield return GoToCor(currentTarget);
            currentTargetIndex = repeatRoute ? (currentTargetIndex + 1) % path.PathCheckpoints.Count : currentTargetIndex + 1;
            yield return new WaitForSeconds(1f);
        }
        stopped = true;
    }    

    public void GoToAndDestroy(Transform target) {
        StartCoroutine(GoToAndDestroyCor(target));
    }

    public IEnumerator GoToAndDestroyCor(Transform target){
        currentTarget = target;
        yield return GoToCor(target);
        Destroy(gameObject);
    }

    public void GoTo(Transform target) {
        currentTarget = target;
        StartCoroutine(GoToCor(currentTarget));
    }

    public IEnumerator GoToCor(Transform target) {
        _hasArrivedToLastLocation = false;
        NavMeshPath pathToPosition = new NavMeshPath();
        navMeshAgent.CalculatePath(target.position, pathToPosition);
        // We estimate the time to target and give the character an extra 8 seconds to finish going to target position
        float estimatedTravelTimeToTarget = GetPathLength(pathToPosition) / navMeshAgent.speed, elapsedTravelTime = 0;
        float ponderatedTravelTimeToTarget = estimatedTravelTimeToTarget + pathTravelCancellationTimeTolerance + (pathTravelCancellationTimeTolerancePerCorner * pathToPosition.corners.Length);
        navMeshAgent.SetDestination(target.position);
        m_animator.StartWalking();
        while(elapsedTravelTime < ponderatedTravelTimeToTarget &&
            (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance
                                                                   || navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete))
        {
            yield return new WaitForSeconds(0.1f);
            elapsedTravelTime += 0.1f;
        }        
        m_animator.StopWalking();
        // Rotate towards the target direction.
        m_animator.StartTurningRight();
        yield return RotateTowardsTargetDirection(target, 1f);
        m_animator.StopTurningRight();
        _hasArrivedToLastLocation = true;
    }   

    public float GetPathLength(NavMeshPath navMeshPath)
    {
        float length = 0;
        for (int i = 0; i < navMeshPath.corners.Length-1; i++) {
            length += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i+1]);
        }
        return length;
    }
    

    public IEnumerator RotateTowardsTargetDirection(Transform targetWithDirection, float seconds) {
        float time = 0;
        while (time<seconds) {
            time += Time.deltaTime;
            // Create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(targetWithDirection.forward);

            // Rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
