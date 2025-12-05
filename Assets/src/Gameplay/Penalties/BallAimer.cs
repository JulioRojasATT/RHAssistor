using UnityEngine;
using UnityEngine.Events;

public class BallAimer : MonoBehaviour
{
    [Header("Horizontal Target Movement")]
    [SerializeField] private GameObject target;

    [SerializeField] private Transform horizontalTargetLeftSide;

    [SerializeField] private Transform horizontalTargetRightSide;

    [SerializeField] private float horizontalTargetSpeed;

    private float _deltaX;

    private Vector3 fixedHorizontalPosition;

    private Vector3 fixedHorizontalTopPosition;

    [SerializeField] private BallShooterState state;

    [Header("Vertical Target Movement")]
    [SerializeField] private Transform verticalTargetTop;

    [SerializeField] private Transform verticalTargetBottom;

    [SerializeField] private float verticalTargetSpeed;    

    private float _deltaY;

    [Header("Gameplay Events")]
    [SerializeField] private UnityEvent onShotLaunched;

    [SerializeField] private UnityEvent onShotSuccess;

    [SerializeField] private UnityEvent onShotFailed;

    [SerializeField] private UnityEvent onShotForced;

    public enum BallShooterState
    {
        MOVING_HORIZONTAL,
        MOVING_VERTICAL,
        INACTIVE
    }

    // Update is called once per frame
    void Update()
    {
        if (state == BallShooterState.MOVING_HORIZONTAL)
        {
            _deltaX = _deltaX + Time.deltaTime * horizontalTargetSpeed;
            target.transform.position = Vector3.Lerp(horizontalTargetLeftSide.position, horizontalTargetRightSide.position, (1 + Mathf.Sin(_deltaX)) / 2f);
            if(Input.GetMouseButtonDown(0))
            {
                state = BallShooterState.INACTIVE;
                ShootBall();
                /*fixedHorizontalPosition = target.transform.position;
                fixedHorizontalTopPosition = fixedHorizontalPosition + Vector3.up * (verticalTargetTop.position.y - verticalTargetBottom.position.y);
                state = BallShooterState.MOVING_VERTICAL;*/
            }
        } /*else if (state == BallShooterState.MOVING_VERTICAL)
        {            
            target.transform.position = Vector3.Lerp(fixedHorizontalPosition, fixedHorizontalTopPosition, (1 + Mathf.Sin(_deltaY)) / 2f);
            _deltaY = _deltaY + Time.deltaTime * verticalTargetSpeed;
            if (Input.GetMouseButtonDown(0))
            {
                state = BallShooterState.INACTIVE;
                ShootBall();
            }
        }*/
    }

    public void SetStateToInactive()
    {
        state = BallShooterState.INACTIVE;
    }

    public void ResetState()
    {
        state = BallShooterState.MOVING_HORIZONTAL;
    }

    public void ShootBallForced()
    {
        onShotForced.Invoke();
    }

    public void ShootBall()
    {
        onShotLaunched.Invoke();
    }
}
