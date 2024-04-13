using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageState : BaseState
{
    public PlayerDamageState(PlayerStateMachine stateMachine) : base("Damage", stateMachine) 
    {

    }

    public override void Enter() {
        if(((PlayerStateMachine)stateMachine).playerDamageable.currentHealth <= 0)
        {
            ((PlayerStateMachine)stateMachine).gameObject.SetActive(false);
        }
    }

    public override void UpdateLogic() {
        if(((PlayerStateMachine)stateMachine).invencibilityTime == 0)
        {
            stateMachine.ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        if(((PlayerStateMachine)stateMachine).invencibilityTime > 0)
        {
            ((PlayerStateMachine)stateMachine).invencibilityTime -= Time.deltaTime;
        }
        else
        {
            ((PlayerStateMachine)stateMachine).invencibilityTime = 0;
        }
    }
}
