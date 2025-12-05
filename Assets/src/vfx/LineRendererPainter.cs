using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererPainter : MonoBehaviour
{
    [SerializeField] private float timeToShowCompletely;

    private Vector3[] points;

    private IEnumerator PartialPaintLineRendererPoints(LineRenderer lineRenderer)
    {        
        Vector3[] linePositions = null;
        Vector3[] totalPositions = new Vector3[lineRenderer.GetPositions(linePositions)];
        Vector3 currentPointPosition = Vector3.zero;
        totalPositions[0] = lineRenderer.GetPosition(0);
        totalPositions[1] = currentPointPosition;
        int currentIndex = 0;
        while (currentIndex < linePositions.Length) {
            currentPointPosition = linePositions[currentIndex];
            lineRenderer.SetPositions(totalPositions);
            yield return new WaitForEndOfFrame();
            currentIndex++;
        }
    }
}
