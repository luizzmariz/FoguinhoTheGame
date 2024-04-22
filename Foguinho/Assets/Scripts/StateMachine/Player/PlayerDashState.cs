using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : BaseState
{
    public PlayerDashState(PlayerStateMachine stateMachine) : base("Dash", stateMachine) {
        
    }

    public override void Enter() {
        ((PlayerStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
        ((PlayerStateMachine)stateMachine).canMove = false;
        ((PlayerStateMachine)stateMachine).canAttack = false;
        ((PlayerStateMachine)stateMachine).isDashing = true;
        Dash();
    }

    public override void UpdateLogic() {
        if(!((PlayerStateMachine)stateMachine).isDashing)
        {
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {

    }

    public void Dash()
    {
        // Vector3 targetPoint = ((PlayerStateMachine)stateMachine).transform.position;

        // if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Keyboard&Mouse")
        // {
        //     //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
        //     Plane playerPlane = new Plane(Vector3.up, new Vector3(0, targetPoint.y, 0));
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     float hitDist;

        //     Debug.DrawRay(ray.origin, ray.direction * 50, Color.blue, 50);

        //     if(playerPlane.Raycast(ray, out hitDist))
        //     {
        //         targetPoint = ray.GetPoint(hitDist);
        //         ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
        //     }
        // }
        // else if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Gamepad")
        // {
        //     Vector2 lookDirection = ((PlayerStateMachine)stateMachine).playerInput.actions["move"].ReadValue<Vector2>();

        //     //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
        //     targetPoint = new Vector3(((PlayerStateMachine)stateMachine).transform.position.x + lookDirection.x * 10, targetPoint.y, ((PlayerStateMachine)stateMachine).transform.position.z + lookDirection.y * 10);
        //     ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
        // }

        Vector2 dashDirection = ((PlayerStateMachine)stateMachine).playerInput.actions["move"].ReadValue<Vector2>();

        // if(((PlayerStateMachine)stateMachine).attackType == 1)
        // {
        //     ((PlayerStateMachine)stateMachine).weaponManager.PrimaryAttack();        
        // }
        // else if(((PlayerStateMachine)stateMachine).attackType == 2)
        // {
        //     ((PlayerStateMachine)stateMachine).weaponManager.SecondaryAttack(targetPoint - ((PlayerStateMachine)stateMachine).transform.position);  
        // }
    }
}
