using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    private Animator _animator;

    private int WALK_BOOL_ID = Animator.StringToHash("Walk"),
        TURN_BOOL_ID = Animator.StringToHash("TurnRight"),
        MOVE_SPEED_FLOAT_ID = Animator.StringToHash("MovementSpeed");

    private float animatorWalkSpeed = 1f, runSpeed = 3f;

    private void Awake(){
        _animator = GetComponent<Animator>();
    }

    public void SetMovementSpeed(float speed) {
        _animator.SetFloat(MOVE_SPEED_FLOAT_ID, speed);
    }

    public void StartWalking(){
        _animator.SetBool(WALK_BOOL_ID, true);
    }

    public void StopWalking(){
        _animator.SetBool(WALK_BOOL_ID, false);
    }

    public void SetSpeed(float speed)
    {

    }

    public void StopTurningRight(){
        _animator.SetBool(TURN_BOOL_ID, false);
    }

    public void StartTurningRight(){
        _animator.SetBool(TURN_BOOL_ID, true);
    }
}
