using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    public MovementState moveState;
    Vector2 moveVector;
    public Vector3 direction;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base("Attack", stateMachine) {

    }

    public override void Enter() {
        ((PlayerStateMachine)stateMachine).canMove = false;
        ((PlayerStateMachine)stateMachine).isAttacking = true;
        ((PlayerStateMachine)stateMachine).attackCooldownTimer = ((PlayerStateMachine)stateMachine).attackDuration;
    }

    public override void UpdateLogic() {

    }

    public override void UpdatePhysics() {

    }
}
