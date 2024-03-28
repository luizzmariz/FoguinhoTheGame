using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    //public Grid grid;
    public Vector3 holderPosition;
    public Vector3 playerPosition;
    public bool hasAskedPath = false;
    public bool followingPath = false;

    int targetIndex;
    Vector3[] path;

    public ChaseState(TestStateMachine stateMachine) : base("Chase", stateMachine) {
        //sm = (NPCEnemySM)stateMachine;
        //grid = stateMachine.grid;
    }

    public override void Enter() {
        //base.Enter();
        //sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        Debug.Log("opa");
        hasAskedPath = false;
        followingPath = false;
    }

    //This function runs at Update()
    public override void UpdateLogic() {
        //base.UpdateLogic();
        
        holderPosition = ((TestStateMachine)stateMachine).transform.position;
        playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;
        
        //ACHO Q PRECISO MELHORAR ESSAS CHECAGENS DE TROCA DE STATE, PQ TA MEIO ZOADO.


        if(Vector3.Distance(holderPosition, playerPosition) > ((TestStateMachine)stateMachine).rangeOfView)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
            ((TestStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
            ((TestStateMachine)stateMachine).animator.SetBool("isMoving", false);
        }
        else if(Vector3.Distance(holderPosition, playerPosition) < ((TestStateMachine)stateMachine).rangeOfAttack)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).chargingState);
            ((TestStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
            ((TestStateMachine)stateMachine).animator.SetBool("isMoving", false);
        }
    }

    //This function runs at LateUpdate()
    public override void UpdatePhysics() {
        //base.UpdatePhysics();

        holderPosition = ((TestStateMachine)stateMachine).transform.position;
        playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;

        if(!hasAskedPath && !followingPath)
        {
            hasAskedPath = true;
            ((TestStateMachine)stateMachine).pathRequestManager.RequestPath(holderPosition, playerPosition, OnPathFound);
        }
        else if(followingPath)
        {
            FollowPath();
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
            for(int i = 0; i < newPath.Length; i++)
            {
                newPath[i].y = 5;
                Debug.Log("wayPoint " + i + " is: " + newPath[i]);
            }
            targetIndex = 0;
            hasAskedPath = false;
            followingPath = true;
			path = newPath;
		}
        else
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        }
	}

    public void FollowPath() 
    {
        ((TestStateMachine)stateMachine).animator.SetBool("isMoving", true);
		Vector3 currentWaypoint = path[0];
        
		if (Vector3.Distance(holderPosition, currentWaypoint) <= 0.1) 
        {
            //Debug.Log("AM I  BECOMING FUICKIN CRAZY??? tagetIndex = " + targetIndex);
			targetIndex ++;
			if(targetIndex >= path.Length) 
            {
                followingPath = false;
                return;
			}
			currentWaypoint = path[targetIndex];
		}
            
        Vector3 movementDirection = new Vector3(currentWaypoint.x - holderPosition.x, 0, currentWaypoint.z - holderPosition.z);
        ((TestStateMachine)stateMachine).rigidBody.velocity = movementDirection.normalized * ((TestStateMachine)stateMachine).movementSpeed;
	}

	// public IEnumerator FollowPath(Vector3[] newPath) 
    // {
    //     ((TestStateMachine)stateMachine).animator.SetBool("isMoving", true);
    //     path = newPath;
    //     targetIndex = 0;
	// 	Vector3 currentWaypoint = path[0];
	// 	while (true) {
	// 		if (holderPosition == currentWaypoint) {
	// 			targetIndex ++;
	// 			if (targetIndex >= path.Length) {
    //                 hasAskedPath = false;
	// 				yield break;
	// 			}
	// 			currentWaypoint = path[targetIndex];
	// 		}

	// 		//holderPosition = Vector3.MoveTowards(holderPosition, currentWaypoint, ((TestStateMachine)stateMachine).movementSpeed * Time.deltaTime);

    //         Vector3 movementDirection = new Vector3(currentWaypoint.x - holderPosition.x, 0, currentWaypoint.z - holderPosition.z);
    //         ((TestStateMachine)stateMachine).rigidBody.velocity = movementDirection.normalized * ((TestStateMachine)stateMachine).movementSpeed;
    //         Debug.Log("holderPosition from Coroutine: " + holderPosition);
	// 		yield return null;
	// 	}
    //     // yield return null;
	// }

	public void OnDrawGizmos() 
    {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(holderPosition, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
