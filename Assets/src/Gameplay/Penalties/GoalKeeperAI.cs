using System.Collections;
using UnityEngine;

public class GoalKeeperAI : MonoBehaviour {

    [SerializeField] private float timeToTarget;

    [SerializeField] private float currentTime;

    [SerializeField] private AnimationCurve speedOverTime;

    public void LaunchToTarget(Transform target) {
        StartCoroutine(LaunchToTargetCor(target));
    }
    
    public IEnumerator LaunchToTargetCor(Transform target) {
        Vector3 initialPosition = transform.position, targetPosition = target.position;
        currentTime = 0;
        while (currentTime<timeToTarget) {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, currentTime / timeToTarget);
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
