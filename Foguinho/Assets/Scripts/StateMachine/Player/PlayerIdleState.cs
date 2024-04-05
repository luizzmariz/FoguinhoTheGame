using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{

    public PlayerIdleState(PlayerStateMachine stateMachine) : base("Idle", stateMachine) {
        
    }

    public override void Enter() {
        
    }

    public override void UpdateLogic() {
        Vector2 moveVector = ((PlayerStateMachine)stateMachine).playerInput.actions["move"].ReadValue<Vector2>();

        if(moveVector != Vector2.zero)
        {
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).moveState);
        }
    }

    public override void UpdatePhysics() {

    }
}
