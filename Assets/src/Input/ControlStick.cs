using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlStick : ScreenSizeAdapter, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector2 currentScreenTouchPosition;

    [SerializeField] private Vector2 movementDirection;

    [SerializeField] private RectTransform stickCircle;

    [SerializeField] private RectTransform stickCenter;

    [SerializeField] private float stickMaxDistance;

    [SerializeField] private BoolScriptableValue isWebGLMobile;

    public Vector2 MovementDirection => movementDirection;

    public float Horizontal => MovementDirection.x;
    public float Vertical => MovementDirection.y;

    public bool IsMovementZero => movementDirection == Vector2.zero;

    private bool _isCursorDown;

    private int _touchNumberThatActivatedStick;

    [Header("Input System Replacement")]

    [SerializeField] private Vector2 mousePosition;

    private int touchCount;

    [SerializeField] private InputActionReference onAnyTouchPerformed;

    [SerializeField] private InputActionReference touchPositionAction;

    [SerializeField] private InputActionReference[] touchPassthroughActions;

    [SerializeField] private Vector2[] touchPositions;

    [Header("Input Start/End Events")]
    [SerializeField] private UnityEvent onInputStarted;

    [SerializeField] private UnityEvent onInputEnded;

    private void Awake()
    {
        touchPositions = new Vector2[touchPassthroughActions.Length];
        for(int i = 0;i < touchPassthroughActions.Length;i++)
        {
            int uncapturedIndex = i;
            touchPassthroughActions[i].action.performed += UpdateTouchPosition;
        }
        touchPositionAction.action.performed += onTouchPositionPerformed;
        onAnyTouchPerformed.action.performed += x => touchCount++;
        onAnyTouchPerformed.action.canceled += x => touchCount--;
    }

    public void UpdateTouchPosition(InputAction.CallbackContext callbackContext){       
        mousePosition = callbackContext.ReadValue<Vector2>();
    }

    public void onTouchPositionPerformed(InputAction.CallbackContext callbackContext){
        mousePosition = callbackContext.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (!_isCursorDown)
        {
            movementDirection = Vector2.zero;
            return;
        }
        Vector2 cursorPosition = mousePosition;
        if (touchCount > 0)
        {
            cursorPosition = mousePosition;
            //cursorPosition = touchPositions[_touchNumberThatActivatedStick];
            //Debug.Log("Going to touch position at index " + _touchNumberThatActivatedStick);
        } else
        {
            //Debug.Log("No touches detected. Fallback to mouse Position");
        }
        // Reset stick positions if we are in web GL and no touches are detected
        float directionMagnitude = Vector2.Distance(cursorPosition, GetRectTransformScreenPosition(stickCenter));
        float magnitudeFactor = directionMagnitude / stickMaxDistance;
        directionMagnitude /= stickMaxDistance;
        movementDirection = Vector3.Normalize(cursorPosition - GetRectTransformScreenPosition(stickCenter));
        Vector2 normalizedDirection = Vector3.Normalize(cursorPosition - GetRectTransformScreenPosition(stickCenter));
        movementDirection *= magnitudeFactor;
        movementDirection = Vector2.ClampMagnitude(movementDirection, stickMaxDistance);
        if (Vector3.Distance(stickCenter.position, cursorPosition) > stickMaxDistance)
        {
            stickCircle.transform.position = (Vector2) stickCenter.transform.position + normalizedDirection * stickMaxDistance;
            return;
        }
        stickCircle.transform.position = cursorPosition;

        for (int i = 0; i < touchCount; i++)
        {
            /*if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                OnTouchCountChanged(i);
            }*/
        }        
    }   

    public void OnPointerUp(PointerEventData eventData)
    {
        _isCursorDown = false;
        stickCircle.transform.position = stickCenter.transform.position;
        onInputEnded.Invoke();        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isCursorDown = true;
        StartCoroutine(RecordTouchNumberAfterOneFrame());
        onInputStarted.Invoke();
    }

    public void ResetStickPosition()
    {
        stickCircle.transform.position = stickCenter.transform.position;
    }

    public IEnumerator RecordTouchNumberAfterOneFrame()
    {
        yield return new WaitForEndOfFrame();
        _touchNumberThatActivatedStick = touchCount - 1;
    }

    public void OnTouchCountChanged(int liftedTouchNumber)
    {
        if (_touchNumberThatActivatedStick > liftedTouchNumber)
        {
            _touchNumberThatActivatedStick--;
        }
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

    public override void AutoAdapt()
    {
        return;
    }
}