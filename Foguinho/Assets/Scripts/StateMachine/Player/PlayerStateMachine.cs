using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //States
    [HideInInspector] public PlayerIdleState idleState;
    // [HideInInspector] public MoveState moveState;
    // [HideInInspector] public AttackState attackState;
    // [HideInInspector] public DamageState damageState;
    // [HideInInspector] public InteractState interactState;
    // [HideInInspector] public DeadState deadState;

    //Global information

    
    //GameObject information
    [Header("Holder Components")]
    public Rigidbody rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Collider collider;
    
    [Header("Attributes")]
    [Range(0f, 50f)] public float rangeOfView;
    [Range(0f, 25f)] public float rangeOfAttack;
    [Range(0f, 10f)] public float movementSpeed;
    public float life;
    public float damage;
    public bool attacking;
    public float attackCooldownTimer;
    public float attackDuration;

    // [Header("Attack")]
    // public string typeOfAttack;

    private void Awake() {
        GetInfo();

        idleState = new PlayerIdleState(this);
        // chaseState = new ChaseState(this);
        // hitState = new HitState(this);
        // chargingState = new ChargingState(this);
    }

    protected override void HelpUpdate()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            attackCooldownTimer = 0;
        }
    }

    public void GetInfo()
    {
        rigidBody = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void OnFire()
    {
        if(attackCooldownTimer == 0)
        {
            //GenerateAttack();
            attacking = true;
            attackCooldownTimer = attackDuration;
        }
    }

    public void OnInteractWithAmbient()
    {
        // if(closestCollider != null && !dialogueManager.animator.GetBool("DialogueBoxIsOpen"))
        // {
        //     closestCollider.GetComponent<Interactable>().BaseInteract();
        // }
    }

    public void OnEnter()
    {
        // if(dialogueManager.animator.GetBool("DialogueBoxIsOpen"))
        // {
        //     dialogueManager.DisplayNextSentence();
        // }
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
