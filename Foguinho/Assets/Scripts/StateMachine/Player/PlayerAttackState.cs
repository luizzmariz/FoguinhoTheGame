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
        ((PlayerStateMachine)stateMachine).isAttacking = true;
        ((PlayerStateMachine)stateMachine).attackCooldownTimer = ((PlayerStateMachine)stateMachine).attackDuration;

        SetAttack();
    }

    public override void UpdateLogic() {
        if(!((PlayerStateMachine)stateMachine).isAttacking)
        {
            ((PlayerStateMachine)stateMachine).ChangeState(((PlayerStateMachine)stateMachine).idleState);
        }
    }

    public override void UpdatePhysics() {
        // //Checks to see if Backspace key is pressed which I define as melee attack; you can of course change this to anything you would want
        // if (Input.GetKeyDown(KeyCode.Backspace))
        // {
        //     //Sets the meleeAttack bool to true
        //     meleeAttack = true;
        // }
        // else
        // {
        //     //Turns off the meleeAttack bool
        //     meleeAttack = false;
        // }
        // //Checks to see if meleeAttack is true and pressing up
        // if (meleeAttack && Input.GetAxis("Vertical") > 0)
        // {
        //     // // Turns on the animation for the player to perform an upward melee attack
        //     // anim.SetTrigger("UpwardMelee");

        //     //Turns on the animation on the melee weapon to show the swipe area for the melee attack upwards
        //     meleeAnimator.SetTrigger("UpwardMeleeSwipe");
        // }
        // //Checks to see if meleeAttack is true and pressing down while also not grounded
        // if (meleeAttack && Input.GetAxis("Vertical") < 0
        //  //&& !character.isGrounded
        //  )
        // {
        //     // //Turns on the animation for the player to perform a downward melee attack
        //     // anim.SetTrigger("DownwardMelee");

        //     //Turns on the animation on the melee weapon to show the swipe area for the melee attack downwards
        //     meleeAnimator.SetTrigger("DownwardMeleeSwipe");
        // }
        // //Checks to see if meleeAttack is true and not pressing any direction
        // if ((meleeAttack && Input.GetAxis("Vertical") == 0)
        //      //OR if melee attack is true and pressing down while grounded
        //     || meleeAttack && (Input.GetAxis("Vertical") < 0
        //      //&& character.isGrounded
        //      ))
        // {
        //     // //Turns on the animation for the player to perform a forward melee attack
        //     // anim.SetTrigger("ForwardMelee");

        //     //Turns on the animation on the melee weapon to show the swipe area for the melee attack forwards
        //     meleeAnimator.SetTrigger("ForwardMeleeSwipe");
        // }
    }

    public void SetAttack()
    {
        if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
            Plane playerPlane = new Plane(Vector3.up, new Vector3(0, 5, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist;

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.blue, 50);

            if(playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
            }
        }
        else if(((PlayerStateMachine)stateMachine).playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 lookDirection = ((PlayerStateMachine)stateMachine).playerInput.actions["look"].ReadValue<Vector2>();

            //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
            Vector3 targetPoint = new Vector3(((PlayerStateMachine)stateMachine).transform.position.x + lookDirection.x * 10, 5, ((PlayerStateMachine)stateMachine).transform.position.z + lookDirection.y * 10);
            ((PlayerStateMachine)stateMachine).characterOrientation.ChangeOrientation(targetPoint);
        }

        ((PlayerStateMachine)stateMachine).weaponManager.PrimaryAttack();        
    }
}
