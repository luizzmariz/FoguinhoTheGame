using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //How much damage the melee attack does
    [SerializeField] private int damageAmount;
    //public PlayerStateMachine playerStateMachine;
    public float timeOfLife;

    public void Start()
    {
        // GetComponent<Rigidbody>().velocity = velocity;
        //playerStateMachine = transform.parent.GetComponentInParent<PlayerStateMachine>();
    }

    void Update()
    {
        if(timeOfLife > 0)
        {
            timeOfLife -= Time.deltaTime;
        }
        else
        {
            AutoDestroy();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Checks to see if the GameObject the MeleeWeapon is colliding with has an EnemyHealth script
        if (collision.GetComponent<EnemyDamageable>())
        {
            // //Method that checks to see what force can be applied to the player when melee attacking
            // HandleCollision(collision.GetComponent<EnemyHealth>());
            
            //Deals damage in the amount of damageAmount
            collision.GetComponent<EnemyDamageable>().Damage(damageAmount);
            // //Coroutine that turns off all the bools related to melee attack collision and direction
            // StartCoroutine(NoLongerColliding());
        }
        if(!collision.GetComponent<PlayerDamageable>())
        {
            Destroy(this.gameObject);
        }
    }

    public void AutoDestroy()
    {
        //playerStateMachine.CastAttackEnded();
        Destroy(this.gameObject);
    }
}