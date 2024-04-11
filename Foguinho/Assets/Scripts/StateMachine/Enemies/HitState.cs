using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BaseState
{
    public float invencibilityTime;
    public HitState(TestStateMachine stateMachine) : base("Hit", stateMachine)
    {
        invencibilityTime = 0.2f;
    }

    public override void Enter() {
        if(((TestStateMachine)stateMachine).enemyDamageable.currentHealth <= 0)
        {
            ((TestStateMachine)stateMachine).gameObject.SetActive(false);
        }
    }

    public override void UpdateLogic() {
        if(invencibilityTime == 0)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        
        if(invencibilityTime > 0)
        {
            invencibilityTime -= Time.deltaTime;
        }
        else
        {
            invencibilityTime = 0;
        }
    }
}
