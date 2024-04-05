using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    //States
    [HideInInspector] public PlayerIdleState idleState;
    [HideInInspector] public PlayerMoveState moveState;
    [HideInInspector] public PlayerAttackState attackState;
    // [HideInInspector] public DamageState damageState;
    [HideInInspector] public PlayerInteractState interactState;
    // [HideInInspector] public DeadState deadState;

    //Global information
    
    //GameObject information
    [Header("Holder Components")]
    public PlayerInput playerInput;
    public Rigidbody rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CharacterOrientation characterOrientation;

    [Header("Attributes")]
    public float runningMultiplier;
    public float movementSpeed;
    public float attackCooldownTimer;
    public float attackDuration;

    [Header("Bools")]
    public bool canMove;
    // public bool isMoving;
    public bool canAttack;
    public bool isAttacking;
    //public bool canAttackWhileMove;

    private void Awake() {
        GetInfo();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        interactState = new PlayerInteractState(this);
        // hitState = new HitState(this);
    }

    protected override void HelpUpdate()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            attackCooldownTimer = 0;
        }
    }

    public void GetInfo()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterOrientation = GetComponent<CharacterOrientation>();
    }

    protected override BaseState GetInitialState() {
        return idleState;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }

    public void OnMove()
    {
        if(canMove)
        {
            ChangeState(moveState);
        }
    }

    public void OnFire()
    {
        if(canAttack)
        {
            if(attackCooldownTimer == 0)
            {
                ChangeState(attackState);
            }
        }
    }

    public void ChargingAttackSucessfull()
    {
        // if(currentState == chargingState)
        // {
        //     chargingState.Attack();
        // }
    }

    public void CastAttackEnded()
    {
        // chargingState.AttackEnded();
    }
}