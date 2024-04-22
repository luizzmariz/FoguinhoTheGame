using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{
    public PlayerDashState(PlayerStateMachine stateMachine) : base("Dash", stateMachine) {
        
    }

    public override void Enter() {
        // ((PlayerStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
        ((PlayerStateMachine)stateMachine).canMove = false;
        ((PlayerStateMachine)stateMachine).canAttack = false;
        ((PlayerStateMachine)stateMachine).canDash = false;
        ((PlayerStateMachine)stateMachine).isDashing = true;
        ((PlayerStateMachine)stateMachine).StartCoroutine(Dash());
    }

    public override void UpdateLogic() {
        if(!((PlayerStateMachine)stateMachine).isDashing)
        {
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {

    }

    public IEnumerator Dash()
    {

        Vector2 dashDirectionV2 = ((PlayerStateMachine)stateMachine).playerInput.actions["move"].ReadValue<Vector2>();
        Vector3 dashDirectionV3 = new Vector3(dashDirectionV2.x, 0, dashDirectionV2.y);
        ((PlayerStateMachine)stateMachine).rigidBody.velocity = dashDirectionV3.normalized * ((PlayerStateMachine)stateMachine).dashingPower;
        ((PlayerStateMachine)stateMachine).trailRenderer.emitting = true;

        // if(((PlayerStateMachine)stateMachine).attackType == 1)
        // {
        //     ((PlayerStateMachine)stateMachine).weaponManager.PrimaryAttack();        
        // }
        // else if(((PlayerStateMachine)stateMachine).attackType == 2)
        // {
        //     ((PlayerStateMachine)stateMachine).weaponManager.SecondaryAttack(targetPoint - ((PlayerStateMachine)stateMachine).transform.position);  
        // }
        yield return new WaitForSeconds(((PlayerStateMachine)stateMachine).dashingTime);

        ((PlayerStateMachine)stateMachine).trailRenderer.emitting = false;
        ((PlayerStateMachine)stateMachine).isDashing = false;
        ((PlayerStateMachine)stateMachine).canMove = true;
        ((PlayerStateMachine)stateMachine).canAttack = true;
        ((PlayerStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;

        yield return new WaitForSeconds(((PlayerStateMachine)stateMachine).dashCooldownTimer);

        ((PlayerStateMachine)stateMachine).canDash = true;
    }
}
