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
    
    [Header("Attributes")]
    [Range(0f, 50f)] public float rangeOfView;
    [Range(0f, 25f)] public float rangeOfAttack;
    [Range(0f, 10f)] public float movementSpeed;
    public float life;
    public float damage;

    // [Header("Attack")]
    // public string typeOfAttack;

    private void Awake() {
        GetInfo();

        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        hitState = new HitState(this);
        chargingState = new ChargingState(this);
    }

    public void GetInfo()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        playerGameObject = GameObject.Find("Player");
        pathRequestManager = GameObject.Find("PathfindingManager").GetComponent<PathRequestManager>();
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

    // public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
	// 	if (pathSuccessful) {
	// 		StopCoroutine(chaseState.FollowPath(newPath));
	// 		StartCoroutine(chaseState.FollowPath(newPath));
	// 	}
	// }

    // public void StopMovementCoroutine() {
	// 	StopCoroutine(chaseState.FollowPath(new Vector3[0]));
    //     Debug.Log("stopping");
	// }
}
