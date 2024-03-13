// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class IdleState : BaseState
// {

//     public IdleState(TestStateMachine stateMachine) : base("Idle", stateMachine) {
//         //sm = (NPCEnemySM)stateMachine;
//     }

//     public override void Enter() {
//         //base.Enter();
//         //sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
//     }

//     public override void UpdateLogic() {
//         //base.UpdateLogic();
        
//         Vector3 holderPosition = ((TestStateMachine)stateMachine).grid.WorldToCell(((TestStateMachine)stateMachine).transform.position);
//         Vector3 playerPosition = ((TestStateMachine)stateMachine).grid.WorldToCell(((TestStateMachine)stateMachine).playerGameObject.transform.position);
        
//         if(Vector3.Distance(holderPosition, playerPosition) <= ((TestStateMachine)stateMachine).rangeOfView)
//         {
//             stateMachine.ChangeState(((TestStateMachine)stateMachine).chaseState);
//         }
//     }

//     public override void UpdatePhysics() {
//         base.UpdatePhysics();

//         //tirar as // dps
//         ((TestStateMachine)stateMachine).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//     }
// }
