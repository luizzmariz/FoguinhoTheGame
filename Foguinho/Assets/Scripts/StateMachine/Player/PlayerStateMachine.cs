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
    [HideInInspector] public PlayerDamageState damageState;
    [HideInInspector] public PlayerInteractState interactState;
    [HideInInspector] public PlayerDashState dashState;
    
    // [HideInInspector] public DeadState deadState;

    //Global information
    
    //GameObject information
    [Header("Holder Components")]
    public PlayerInput playerInput;
    public Rigidbody rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CharacterOrientation characterOrientation;
    public WeaponManager weaponManager;
    public PlayerDamageable playerDamageable;
    public TrailRenderer trailRenderer;

    [Header("Movement")]
    public bool canMove;
    public float runningMultiplier;
    public float movementSpeed;

    [Header("Dash")]
    public bool canDash;
    public bool isDashing;
    public float dashingPower;
    public float dashCooldownTimer;
    public float dashingTime;

    [Header("Attack")]
    public bool canAttack;
    public bool isAttacking;
    public int attackType;
    public float attackDuration;
    public float attack1CooldownTimer;
    public float attack2CooldownTimer;

    [Header("InvencibilityTime")]
    public float invencibilityTime;

    private void Awake() {
        GetInfo();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        interactState = new PlayerInteractState(this);
        damageState = new PlayerDamageState(this);
        dashState = new PlayerDashState(this);
    }

    protected override void HelpUpdate()
    {
        if(attack1CooldownTimer > 0)
        {
            attack1CooldownTimer -= Time.deltaTime;
        }
        else
        {
            attack1CooldownTimer = 0;
        }
        if(attack2CooldownTimer > 0)
        {
            attack2CooldownTimer -= Time.deltaTime;
        }
        else
        {
            attack2CooldownTimer = 0;
        }
    }

    public void GetInfo()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterOrientation = GetComponent<CharacterOrientation>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        playerDamageable = GetComponent<PlayerDamageable>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    protected override BaseState GetInitialState() {
        return idleState;
    }

    // private void OnGUI()
    // {
    //     GUILayout.BeginArea(new Rect(10f, 400f, 200f, 100f));
    //     string content = currentState != null ? currentState.name : "(no current state)";
    //     GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    //     GUILayout.EndArea();
    // }

    public void OnMove()
    {
        if(canMove)
        {
            ChangeState(moveState);
        }
    }

    public void OnFire1()
    {
        attackType = 1;
        if(canAttack)
        {
            if(attack1CooldownTimer == 0)
            {
                ChangeState(attackState);
            }
        }
    }

    public void OnFire2()
    {
        attackType = 2;
        if(canAttack)
        {
            if(attack2CooldownTimer == 0)
            {
                ChangeState(attackState);
            }
        }
    }

    public void OnDash()
    {
        if(canDash)
        {
            if(dashCooldownTimer == 0)
            {
                ChangeState(dashState);
            }
        }
    }

    public void OnTrap()
    {   
        attackType = 3;
        if(canAttack)
        {
            ChangeState(attackState);
        }
    }

    //we need to change this after we got some basic character animations - this functions need to be called by the character animations and not by the attack animation
    public void CastAttackEnded()
    {
        isAttacking = false;
    }

    public void DashEnded()
    {
        isDashing = false;
    }
}
