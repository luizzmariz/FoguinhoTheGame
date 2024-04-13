using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    //How much damage the melee attack does
    [SerializeField] private int damageAmount;
    public TestStateMachine testStateMachine;

    public void Start()
    {
        testStateMachine = transform.parent.GetComponentInParent<TestStateMachine>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Checks to see if the GameObject the MeleeWeapon is colliding with has an EnemyHealth script
        if (collision.GetComponent<PlayerDamageable>())
        {
            // //Method that checks to see what force can be applied to the player when melee attacking
            // HandleCollision(collision.GetComponent<EnemyHealth>());
            
            //Deals damage in the amount of damageAmount
            collision.GetComponent<PlayerDamageable>().Damage(damageAmount);
            // //Coroutine that turns off all the bools related to melee attack collision and direction
            // StartCoroutine(NoLongerColliding());
        }
    }
}
