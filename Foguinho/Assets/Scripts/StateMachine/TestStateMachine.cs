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
    
    //GameObject information
    [Header("Holder Components")]
    public Rigidbody rigidBody;
    
    [Header("Base Stats")]
    [Range(0f,10f)] public float rangeOfView;
    [Range(0f,10f)] public float rangeOfAttack;
    [Range(0f,5f)] public float movementSpeed;
    //public Vector2 movementSpeed;
    public float life;
    public float damage;

    [Header("Other's Components")]

    public GameObject playerGameObject;

    private void Awake() {
        GetInfo();

        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        hitState = new HitState(this);
        chargingState = new ChargingState(this);
    }

    public void GetInfo()
    {
        playerGameObject = GameObject.Find("Player");
        pathRequestManager = GameObject.Find("PathfindingManager").GetComponent<PathRequestManager>();

        rigidBody = GetComponent<Rigidbody>();
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

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
            Debug.Log("oi");
			StopCoroutine(chaseState.FollowPath(newPath));
			StartCoroutine(chaseState.FollowPath(newPath));
		}
	}
}
