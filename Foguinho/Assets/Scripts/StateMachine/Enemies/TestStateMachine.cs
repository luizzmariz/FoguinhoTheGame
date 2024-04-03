using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStateMachine : StateMachine
{
    //States
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public HitState hitState;
    [HideInInspector] public ChargingState chargingState;

    //Global information
    [HideInInspector] public PathRequestManager pathRequestManager;
    [HideInInspector] public GameObject playerGameObject;
    
    //GameObject information
    [Header("Holder Components")]
    public Rigidbody rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    [Header("Attributes")]
    [Range(0f, 50f)] public float rangeOfView;
    [Range(0f, 25f)] public float rangeOfAttack;
    [Range(0f, 10f)] public float movementSpeed;
    public float life;
    public float damage;
    public float attackCooldownTimer;
    public float attackDuration;

    // [Header("Attack")]
    // public string typeOfAttack;

    private void Awake() {
        GetInfo();

        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        hitState = new HitState(this);
        chargingState = new ChargingState(this);
    }

    protected override void HelpUpdate()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            attackCooldownTimer = 0;
        }
    }

    public void GetInfo()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerGameObject = GameObject.Find("Player");
        pathRequestManager = GameObject.Find("PathfindingManager").GetComponent<PathRequestManager>();
    }

    protected override BaseState GetInitialState() {
        return idleState;
    }

    // private void OnGUI()
    // {
    //     GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
    //     string content = currentState != null ? currentState.name : "(no current state)";
    //     GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    //     GUILayout.EndArea();
    // }

    public void ChargingAttackSucessfull()
    {
        if(currentState == chargingState)
        {
            chargingState.Attack();
        }
    }

    public void CastAttackEnded()
    {
        chargingState.AttackEnded();
    }
}
