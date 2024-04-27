using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    //How much the player should move either downwards or horizontally when melee attack collides with a GameObject that has EnemyHealth script on it
    public float defaultForce = 300;
    //How much the player should move upwards when melee attack collides with a GameObject that has EnemyHealth script on it
    public float upwardsForce = 600;
    //How long the player should move when melee attack collides with a GameObject that has EnemyHealth script on it
    public float movementTime = .1f;
    //Input detection to see if the button to perform a melee attack has been pressed
    private bool meleeAttack;
    //The animator on the meleePrefab
    //private Animator meleeAnimator;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base("Attack", stateMachine) {
        
    }

    public override void Enter() {
        ((PlayerStateMachine)stateMachine).rigidBody.velocity = Vector3.zero;
        ((PlayerStateMachine)stateMachine).canMove = false;
        ((PlayerStateMachine)stateMachine).canAttack = false;
        ((PlayerStateMachine)stateMachine).isAttacking = true;
        if(((PlayerStateMachine)stateMachine).attackType == 1)
        {
            ((PlayerStateMachine)stateMachine).attack1CooldownTimer = ((PlayerStateMachine)stateMachine).attackDuration;

        }
        else if(((PlayerStateMachine)stateMachine).attackType == 2)
        {
            ((PlayerStateMachine)stateMachine).attack2CooldownTimer = ((PlayerStateMachine)stateMachine).attackDuration;

        }
        SetAttack();
    }

    public override void UpdateLogic() {
        if(!((PlayerStateMachine)stateMachine).isAttacking)
        {
            ((PlayerStateMachine)stateMachine).canMove = true;
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {

    }

    public void SetAttack()
    {
        Vector3 targetPoint = ((PlayerStateMachine)stateMachine).transform.position;

        if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
            Plane playerPlane = new Plane(Vector3.up, new Vector3(0, targetPoint.y, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist;

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.blue, 50);

            if(playerPlane.Raycast(ray, out hitDist))
            {
                targetPoint = ray.GetPoint(hitDist);
                ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
            }
        }
        else if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 lookDirection = ((PlayerStateMachine)stateMachine).playerInput.actions["look"].ReadValue<Vector2>();

            targetPoint = new Vector3(((PlayerStateMachine)stateMachine).transform.position.x + lookDirection.x * 10, targetPoint.y, ((PlayerStateMachine)stateMachine).transform.position.z + lookDirection.y * 10);
            ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
        }

        if(((PlayerStateMachine)stateMachine).attackType == 1)
        {
            ((PlayerStateMachine)stateMachine).weaponManager.PrimaryAttack();        
        }
        else if(((PlayerStateMachine)stateMachine).attackType == 2)
        {
            if(targetPoint - ((PlayerStateMachine)stateMachine).transform.position != Vector3.zero)
            {
                ((PlayerStateMachine)stateMachine).weaponManager.SecondaryAttack(targetPoint - ((PlayerStateMachine)stateMachine).transform.position);  
            }
            else
            {
                ((PlayerStateMachine)stateMachine).weaponManager.SecondaryAttack(((PlayerStateMachine)stateMachine).characterOrientation.lastOrientation - ((PlayerStateMachine)stateMachine).transform.position);  
            }
        }
        else if(((PlayerStateMachine)stateMachine).attackType == 3)
        {
            if(((PlayerStateMachine)stateMachine).trapsPlaced < ((PlayerStateMachine)stateMachine).trapsLimit)
            {
                ((PlayerStateMachine)stateMachine).weaponManager.PlaceTrap(targetPoint, ((PlayerStateMachine)stateMachine).transform.position);
                ((PlayerStateMachine)stateMachine).trapsPlaced++;
            }
            else
            {
                ((PlayerStateMachine)stateMachine).CastAttackEnded();
            }
        }
    }
}
