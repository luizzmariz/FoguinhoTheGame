using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : BaseState
{

    public ChargingState(TestStateMachine stateMachine) : base("Charging", stateMachine)
    {

    }

    public override void Enter() {
        base.Enter();

        ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", true);
        Debug.Log("wtf");
    }

    public override void UpdateLogic() {
        Vector3 holderPosition = ((TestStateMachine)stateMachine).transform.position;
        Vector3 playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;

        if(Vector3.Distance(holderPosition, playerPosition) > ((TestStateMachine)stateMachine).rangeOfAttack)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
            ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", false);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
    }

    public void Attack()
    {
        ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", false);
        ((TestStateMachine)stateMachine).animator.SetTrigger("castAttack");
        Debug.Log("Alley Hoo");
    }

    public void AttackEnded()
    {
        ((TestStateMachine)stateMachine).animator.SetTrigger("attackEnd");
        stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        ((TestStateMachine)stateMachine).attackCooldownTimer = ((TestStateMachine)stateMachine).attackDuration;
        Debug.Log("Hoolley");
    }
}
