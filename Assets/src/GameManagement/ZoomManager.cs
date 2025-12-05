using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomManager : MonoBehaviour
{
    private Vector3 previousTouchPosition;

    private float deltaX, deltaY;

    [SerializeField] private UnityEvent onZoomStarted;

    [SerializeField] private UnityEvent onZoomFinished;

    [Header("Translation Parameters")]
    [SerializeField] private RectTransform zoomedImage;    

    [SerializeField] private Vector2 initialZoomImagePosition;

    [SerializeField] private float movementSpeed;

    [Header("Follow parameters")]
    /// <summary>
    /// Zoom circle that follows the mouse cursor
    /// </summary>
    [SerializeField] private RectTransform zoomCircle;

    [SerializeField] private Vector2 zoomCircleFollowOffset;

    private void Update()
    {
        if (!zoomCircle.gameObject.activeSelf)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            onZoomFinished.Invoke();
        } else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchDirection = previousTouchPosition - Input.mousePosition;            
            deltaX = touchDirection.x;
            deltaY = touchDirection.y;
            TryMoveZoomedImage(deltaX,deltaY);
            previousTouchPosition = Input.mousePosition;
            ZoomCircleFollow(previousTouchPosition);
        }
    }

    public void RecordZoomImageInitialPosition()
    {
        initialZoomImagePosition = zoomedImage.anchoredPosition;
        zoomCircleFollowOffset *= Screen.height / 1920f;
    }

    public void ZoomCircleFollow(Vector2 mousePosition)
    {
        if (zoomCircle == null) {
            return;
        }
        zoomCircle.transform.position = mousePosition + zoomCircleFollowOffset;
    }

    public void ResetZoomImagePosition() {
        zoomedImage.anchoredPosition = initialZoomImagePosition;
    }

    public void ActivateZoom() {
        previousTouchPosition = Input.mousePosition;
        onZoomStarted.Invoke();
    }

    public void TryMoveZoomedImage(float moveX, float moveY)
    {
        zoomedImage.transform.position += movementSpeed * new Vector3(moveX, moveY,0);
    }
}
