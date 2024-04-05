using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// public enum MovementState
// {
//     WALKING,
//     RUNNING
// }

public class PlayerMovement : MonoBehaviour
{
    public MovementState moveState;
    private Rigidbody rb;
    [Range(1.5f,3f)] 
    public float runningMultiplier;
    public float speed;
    [HideInInspector]
    public Vector3 direction;
    [SerializeField] private CharacterOrientation characterOrientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveState = MovementState.WALKING;
    }

    public void OnMove()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        moveState = MovementState.WALKING;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal > 0f)
        {
            if(horizontal > 0.6f)
            {
                moveState = MovementState.RUNNING;
            }
            horizontal = 1f;
        }
        else if(horizontal < 0f)
        {
            if(horizontal < -0.6f)
            {
                moveState = MovementState.RUNNING;
            }
            horizontal = -1f;
        }
        if(vertical > 0f)
        {
            if(vertical > 0.6f)
            {
                moveState = MovementState.RUNNING;
            }
            vertical = 1f;
        }
        else if(vertical < 0f)
        {
            if(vertical < -0.6f)
            {
                moveState = MovementState.RUNNING;
            }
            vertical = -1f;
        }

        direction = new Vector3(horizontal, 0, vertical);
    }

    void Move()
    {
        if(moveState == MovementState.RUNNING)
        {
            rb.velocity = direction.normalized * runningMultiplier * speed;
        }
        else
        {
            rb.velocity = direction.normalized * speed;
        }
    }

    void LateUpdate()
    {
        if(GetComponent<PlayerInput>().actions["move"].ReadValue<Vector2>() != Vector2.zero)
        {
            if(characterOrientation != null && !GetComponent<PlayerAttack>().attacking)
            {
                characterOrientation.ChangeOrientation(transform.position + direction * 10);
            }
        }
    }
}
