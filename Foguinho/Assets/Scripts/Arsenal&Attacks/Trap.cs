using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //How much damage the melee attack does
    [SerializeField] public float damageAmount;
    public float timeOfLife;

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
            //Deals damage in the amount of damageAmount
            collision.GetComponent<EnemyDamageable>().Damage(damageAmount);
            Destroy(this.gameObject);
        }
    }

    public void AutoDestroy()
    {
        GameObject.Find("Player").GetComponent<PlayerStateMachine>().trapsPlaced--;
        Destroy(this.gameObject);
    }
}