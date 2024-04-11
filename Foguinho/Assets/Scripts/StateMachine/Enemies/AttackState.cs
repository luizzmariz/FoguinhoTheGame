using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : BaseState
{
    Vector3 holderPosition;
    Vector3 playerPosition;

    public ChargingState(TestStateMachine stateMachine) : base("Charging", stateMachine)
    {

    }

    public override void Enter() {
        base.Enter();

        ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", true);
        
    }

    public override void UpdateLogic() {
        holderPosition = ((TestStateMachine)stateMachine).transform.position;
        playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;

        if(Vector3.Distance(holderPosition, playerPosition) > ((TestStateMachine)stateMachine).rangeOfAttack)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
            ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", false);
        }
    }

    public override void UpdatePhysics() {
        ((TestStateMachine)stateMachine).characterOrientation.ChangeOrientation(playerPosition);
    }

    public void Attack()
    {
        ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", false);
        ((TestStateMachine)stateMachine).animator.SetTrigger("castAttack");
    }

    public void AttackEnded()
    {
        ((TestStateMachine)stateMachine).animator.SetTrigger("attackEnd");
        stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        ((TestStateMachine)stateMachine).attackCooldownTimer = ((TestStateMachine)stateMachine).attackDuration;
    }
}
