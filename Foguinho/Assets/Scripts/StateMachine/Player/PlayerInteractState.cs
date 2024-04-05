using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : BaseState
{

    public PlayerInteractState(PlayerStateMachine stateMachine) : base("Interact", stateMachine) {
        
    }

    public override void Enter() {
        ((PlayerStateMachine)stateMachine).canMove = false;
        ((PlayerStateMachine)stateMachine).canAttack = false;
    }

    public override void UpdateLogic() {
        // if(((PlayerStateMachine)stateMachine).isMoving)
        // {
        //     ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).moveState);
        // }
    }

    public override void UpdatePhysics() {

    }

    public void ExitState()
    {
        ((PlayerStateMachine)stateMachine).canMove = true;
        ((PlayerStateMachine)stateMachine).canAttack = true;
        ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
    }
}
