using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable
{
    [SerializeField] float maxHealth;
    private bool damageable = true;
    [SerializeField] private PlayerStateMachine stateMachine;

    // public EnemyDamageable() : base("Move", stateMachine) {
    //     moveState = MovementState.WALKING;
    // }

    public void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        currentHealth = maxHealth;
    }

    public override void Damage(float damageAmount)
    {
        if(damageable)
        {
            currentHealth -= damageAmount;
            stateMachine.ChangeState(stateMachine.damageState);
        }
    }
}
