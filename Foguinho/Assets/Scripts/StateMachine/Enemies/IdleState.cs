using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{

    public IdleState(TestStateMachine stateMachine) : base("Idle", stateMachine) {
        //sm = (NPCEnemySM)stateMachine;
    }

    public override void Enter() {

    }

    public override void UpdateLogic() {
        //base.UpdateLogic();
        
        //ACHO Q PRECISO MELHORAR ESSAS CHECAGENS DE TROCA DE STATE, PQ TA MEIO ZOADO.


        Vector3 holderPosition = ((TestStateMachine)stateMachine).transform.position;
        Vector3 playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;
        
        if(Vector3.Distance(holderPosition, playerPosition) <= ((TestStateMachine)stateMachine).rangeOfAttack)
        {
            if(((TestStateMachine)stateMachine).attackCooldownTimer == 0)
            {
                stateMachine.ChangeState(((TestStateMachine)stateMachine).chargingState);
            }
        }
        else if(Vector3.Distance(holderPosition, playerPosition) <= ((TestStateMachine)stateMachine).rangeOfView)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).chaseState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();

        //tirar as // dps
        ((TestStateMachine)stateMachine).GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
