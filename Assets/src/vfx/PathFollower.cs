using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {
     [SerializeField] private Transform[] pathPoints;
     [SerializeField] private float moveSpeed;
     [SerializeField] private float closenessEpsilon;
     [SerializeField] private int currentPathPointIndex = 0;

     public void PatrolPath() {
          StartCoroutine(MoveCoroutine());
     }

     private IEnumerator MoveCoroutine() {          
          transform.position = pathPoints[0].position;
          Vector3 nextPathPoint = pathPoints[(currentPathPointIndex + 1) % pathPoints.Length].position;
          Vector3 directionToNextPathPoint = Vector3.Normalize(nextPathPoint - pathPoints[currentPathPointIndex].position);
          while (true) {
               if (Vector3.Distance(transform.position, nextPathPoint) >
                   closenessEpsilon) {
                    transform.position += Time.fixedDeltaTime * moveSpeed * directionToNextPathPoint;
               } else {
                    currentPathPointIndex = (currentPathPointIndex + 1) % pathPoints.Length;
                    nextPathPoint = pathPoints[(currentPathPointIndex + 1) % pathPoints.Length].position;
                    directionToNextPathPoint = Vector3.Normalize(nextPathPoint - pathPoints[currentPathPointIndex].position);
               }
               yield return new WaitForFixedUpdate();
          }
     }
}
