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
    }

    public override void UpdateLogic() {
        //base.UpdateLogic();

        // if(!((TestStateMachine)stateMachine).animator.GetCurrentAnimatorStateInfo(0).IsName("TestEnemyAttacking"))
        // {
        //     stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        //     ((TestStateMachine)stateMachine).animator.SetBool("chargingAttack", false);
        // };

        //ACHO Q PRECISO MELHORAR ESSAS CHECAGENS DE TROCA DE STATE, PQ TA MEIO ZOADO.

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
        Debug.Log("Alley Hoo");
    }
}
