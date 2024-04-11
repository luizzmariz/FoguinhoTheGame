using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : Damageable
{
    [SerializeField] float maxHealth;
    private bool damageable = true;
    [SerializeField] private TestStateMachine stateMachine;

    // public EnemyDamageable() : base("Move", stateMachine) {
    //     moveState = MovementState.WALKING;
    // }

    public void Start()
    {
        stateMachine = GetComponent<TestStateMachine>();
        currentHealth = maxHealth;
    }

    public override void Damage(float damageAmount)
    {
        if(damageable)
        {
            currentHealth -= damageAmount;
            stateMachine.ChangeState(stateMachine.hitState);
        }
    }
}
