// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TestStateMachine : StateMachine
// {
//     [HideInInspector]
//     public IdleState idleState;
//     [HideInInspector]
//     public ChaseState chaseState;

//     // [HideInInspector]
//     // public Chasing chasingState;

//     // [HideInInspector]
//     // public Stun stunState;
//     // [HideInInspector]
//     // public Fallen fallenState;

//     // public List<GameObject> targets;
//     // public GameObject curTarget;

//     //public Rigidbody2D rigidBody;
    
//     [Header("Holder Components")]

//     public Rigidbody2D rigidBody2D;
//     public Pathfinding pathfinding;

//     [Header("Base Stats")]

//     [Range(0f,10f)] 
//     public float rangeOfView;
//     [Range(0f,5f)] 
//     public float movementSpeed;
//     //public Vector2 movementSpeed;
//     public float life;
//     public float damage;

//     [Header("Other's Components")]

//     public GameObject playerGameObject;
//     public Grid grid;

//     private void Awake() {
//         GetInfo();

//         idleState = new IdleState(this);
//         chaseState = new ChaseState(this);
//     }

//     public void GetInfo()
//     {
//         playerGameObject = GameObject.Find("Player");
//         grid = GameObject.Find("Grid").GetComponent<Grid>();

//         rigidBody2D = GetComponent<Rigidbody2D>();
//         pathfinding = GetComponent<Pathfinding>();
//     }

//     protected override BaseState GetInitialState() {
//         return idleState;
//     }

//     // void OnCollisionEnter2D(Collision2D collisionInfo) {
//     //     
//     // }

//     // void OnTriggerEnter2D(Collider2D collisionInfo) {
//     //     
//     // }

//     // private void OnGUI()
//     // {
//     //     GUILayout.BeginArea(new Rect(10f, 10f, 200f, 100f));
//     //     string content = currentState != null ? currentState.name : "(no current state)";
//     //     GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
//     //     GUILayout.EndArea();
//     // }
// }
