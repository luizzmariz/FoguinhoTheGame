// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ChaseState : BaseState
// {
//     //public Grid grid;
//     public Vector3 holderPosition;
//     public Vector3 playerPosition;


//     public ChaseState(TestStateMachine stateMachine) : base("Chase", stateMachine) {
//         //sm = (NPCEnemySM)stateMachine;
//         //grid = stateMachine.grid;
//     }

//     public override void Enter() {
//         //base.Enter();
//         //sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
//     }

//     public override void UpdateLogic() {
//         //base.UpdateLogic();
        
//         holderPosition = ((TestStateMachine)stateMachine).grid.WorldToCell(((TestStateMachine)stateMachine).transform.position);
//         playerPosition = ((TestStateMachine)stateMachine).grid.WorldToCell(((TestStateMachine)stateMachine).playerGameObject.transform.position);
        
//         if(Vector3.Distance(holderPosition, playerPosition) > ((TestStateMachine)stateMachine).rangeOfView)
//         {
//             stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
//         }
//     }

//     public override void UpdatePhysics() {
//         base.UpdatePhysics();

//         ((TestStateMachine)stateMachine).pathfinding.target = ((TestStateMachine)stateMachine).playerGameObject.transform;
//         ((TestStateMachine)stateMachine).pathfinding.FindPath(PathFound);

//     }

//     public void PathFound(List<GridNode> path)
//     {
//         if(path.Count > 0)
//         {
//             //((TestStateMachine)stateMachine).transform.position = Vector3.MoveTowards(((TestStateMachine)stateMachine).transform.position, path[0].worldPosition, ((TestStateMachine)stateMachine).movementSpeed * Time.deltaTime);

//             Vector3 directionVector3 = (path[0].worldPosition - ((TestStateMachine)stateMachine).transform.position).normalized;
//             Vector2 directionVector2 = new Vector2(directionVector3.x, directionVector3.y);
//             //Debug.Log(directionVector2);
//             ((TestStateMachine)stateMachine).rigidBody2D.velocity = directionVector2 * ((TestStateMachine)stateMachine).movementSpeed;
//             //Debug.Log(((TestStateMachine)stateMachine).rigidBody2D.velocity);
//         }
//         else
//         {
//             Debug.Log("Hm");
//         }
//     }
// }
