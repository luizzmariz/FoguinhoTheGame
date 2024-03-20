using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    //public Grid grid;
    public Vector3 holderPosition;
    public Vector3 playerPosition;
    public bool hasAskedPath;

    int targetIndex;
    Vector3[] path;

    public ChaseState(TestStateMachine stateMachine) : base("Chase", stateMachine) {
        //sm = (NPCEnemySM)stateMachine;
        //grid = stateMachine.grid;
    }

    public override void Enter() {
        //base.Enter();
        //sm.rigidBody.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
        hasAskedPath = false;
    }

    public override void UpdateLogic() {
        //base.UpdateLogic();
        
        holderPosition = ((TestStateMachine)stateMachine).transform.position;
        playerPosition = ((TestStateMachine)stateMachine).playerGameObject.transform.position;
        
        if(Vector3.Distance(holderPosition, playerPosition) > ((TestStateMachine)stateMachine).rangeOfView)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        }
        else if(Vector3.Distance(holderPosition, playerPosition) < ((TestStateMachine)stateMachine).rangeOfAttack)
        {
            stateMachine.ChangeState(((TestStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();

        if(!hasAskedPath)
        {
            hasAskedPath = true;
            ((TestStateMachine)stateMachine).pathRequestManager.RequestPath(holderPosition, playerPosition, ((TestStateMachine)stateMachine).OnPathFound);
        }
    }

	public IEnumerator FollowPath(Vector3[] newPath) 
    {
        hasAskedPath = false;
        path = newPath;
        targetIndex = 0;
        if(path[0] == null)
        {
            Debug.Log("wtfw");
        }
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (holderPosition == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			holderPosition = Vector3.MoveTowards(holderPosition, currentWaypoint, ((TestStateMachine)stateMachine).movementSpeed * Time.deltaTime);
			yield return null;
		}
	}

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
