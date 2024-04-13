using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    WALKING,
    RUNNING
}

public class PlayerMoveState : BaseState
{
    public MovementState moveState;
    Vector2 moveVector;
    public Vector3 direction;

    public PlayerMoveState(PlayerStateMachine stateMachine) : base("Move", stateMachine) {
        moveState = MovementState.WALKING;
    }

    public override void Enter() {
    
    }

    public override void UpdateLogic() {
        moveVector = ((PlayerStateMachine)stateMachine).playerInput.actions["move"].ReadValue<Vector2>();

        if(moveVector == Vector2.zero)
        {
            ((PlayerStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
            ((PlayerStateMachine)stateMachine).animator.SetBool("isMoving", false);
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        moveState = MovementState.WALKING;

        GetInput();
        Move();
        SendOrientation();

        ((PlayerStateMachine)stateMachine).animator.SetBool("isMoving", true);
    }

    void GetInput()
    {
        float horizontal = moveVector.x;
        float vertical = moveVector.y;
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");

        // if(horizontal > 0f)
        // {
        //     if(horizontal > 0.6f)
        //     {
        //         moveState = MovementState.RUNNING;
        //     }
        //     horizontal = 1f;
        // }
        // else if(horizontal < 0f)
        // {
        //     if(horizontal < -0.6f)
        //     {
        //         moveState = MovementState.RUNNING;
        //     }
        //     horizontal = -1f;
        // }
        // if(vertical > 0f)
        // {
        //     if(vertical > 0.6f)
        //     {
        //         moveState = MovementState.RUNNING;
        //     }
        //     vertical = 1f;
        // }
        // else if(vertical < 0f)
        // {
        //     if(vertical < -0.6f)
        //     {
        //         moveState = MovementState.RUNNING;
        //     }
        //     vertical = -1f;
        // }

        moveState = MovementState.RUNNING;
        direction = new Vector3(horizontal, 0, vertical);
    }

    void Move()
    {
        if(moveState == MovementState.RUNNING)
        {
            ((PlayerStateMachine)stateMachine).rigidBody.velocity = direction.normalized * ((PlayerStateMachine)stateMachine).runningMultiplier * ((PlayerStateMachine)stateMachine).movementSpeed;
        }
        else
        {
            ((PlayerStateMachine)stateMachine).rigidBody.velocity = direction.normalized * ((PlayerStateMachine)stateMachine).movementSpeed;
        }
    }

    void SendOrientation()
    {
        if(!((PlayerStateMachine)stateMachine).isAttacking)
        {
            ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(((PlayerStateMachine)stateMachine).transform.position + direction * 10);
        }
    }
}
