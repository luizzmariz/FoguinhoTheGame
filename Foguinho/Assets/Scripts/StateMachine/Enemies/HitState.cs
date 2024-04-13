using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BaseState
{
    public HitState(TestStateMachine stateMachine) : base("Hit", stateMachine)
    {

    }

    public override void Enter() {
        if(((TestStateMachine)stateMachine).enemyDamageable.currentHealth <= 0)
        {
            ((TestStateMachine)stateMachine).gameObject.SetActive(false);
        }
    }

    public override void UpdateLogic() {
        if(((TestStateMachine)stateMachine).invencibilityTime == 0)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        if(((TestStateMachine)stateMachine).invencibilityTime > 0)
        {
            ((TestStateMachine)stateMachine).invencibilityTime -= Time.deltaTime;
        }
        else
        {
            ((TestStateMachine)stateMachine).invencibilityTime = 0;
        }
    }
}
