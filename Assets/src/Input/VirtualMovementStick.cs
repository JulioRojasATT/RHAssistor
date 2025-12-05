using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualMovementStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] private Vector2 currentScreenTouchPosition;

    [SerializeField] private Vector2 movementDirection;

    [SerializeField] private RectTransform stickCircle;

    [SerializeField] private RectTransform stickCenter;

    [SerializeField] private float stickMaxDistance;

    public Vector2 MovementDirection=>movementDirection;
       
    public float Horizontal => MovementDirection.x;
    public float Vertical => MovementDirection.y;

    public bool IsMovementZero => movementDirection==Vector2.zero;    

    private bool _isCursorDown;

    private bool _isCursorInStickArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down");
        currentScreenTouchPosition = eventData.position;
        _isCursorDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer up");
        //stickCircle.anchoredPosition = GetRectTransformScreenPosition(stickCenter);
        _isCursorDown = false;
    }

    // Update is called once per frame
    void Update() {
        if (!_isCursorDown)
        {            
            movementDirection = Vector2.zero;
            return;
        }
        float directionMagnitude = Vector2.Distance(currentScreenTouchPosition, GetRectTransformScreenPosition(stickCenter));
        directionMagnitude /= stickMaxDistance;
        /*Vector2 inputDirection = ;
        movementDirection = Vector3.Normalize();*/
        movementDirection = Vector3.Normalize(currentScreenTouchPosition - GetRectTransformScreenPosition(stickCenter));
        //stickCircle.anchoredPosition = GetRectTransformScreenPosition(stickCenter) + (currentScreenTouchPosition - GetRectTransformScreenPosition(stickCenter));
    }

    public Rect GetRectTransform2DRect(RectTransform rectTrans)
    {
        Vector2 size = Vector2.Scale(rectTrans.rect.size, rectTrans.lossyScale);
        return new Rect((Vector2)rectTrans.position - (size * rectTrans.pivot), size);
    }

    public Vector2 GetRectTransformScreenPosition(RectTransform rectTrans)
    {
        Vector2 size = Vector2.Scale(rectTrans.rect.size, rectTrans.lossyScale);
        return new Rect((Vector2)rectTrans.position - (size * rectTrans.pivot), size).center;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Pointer dragged");
        currentScreenTouchPosition = eventData.position;
    }
}
