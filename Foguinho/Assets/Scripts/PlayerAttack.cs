using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float cooldownTimer;
    [SerializeField] float attackDuration;
    public bool attacking;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterOrientation characterOrientation;

    void Start()
    {
        attacking = false;
        cooldownTimer = 0;
        if(playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }
    }

    void Update()
    {
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            cooldownTimer = 0;
        }
    }

    public void OnFire()
    {
        if(cooldownTimer == 0)
        {
            GenerateAttack();
            attacking = true;
            cooldownTimer = attackDuration;
        }
    }

    void GenerateAttack()
    {
        if(playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
            Plane playerPlane = new Plane(Vector3.up, new Vector3(0, 5, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist;

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.blue, 50);

            if(playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                characterOrientation.ChangeOrientation(targetPoint);
            }
        }
        else if(playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 lookDirection = playerInput.actions["look"].ReadValue<Vector2>();

            //this "new Vector3(x, 5, x) bellow is this way because of the height level of the character
            Vector3 targetPoint = new Vector3(transform.position.x + lookDirection.x * 10, 5, transform.position.z + lookDirection.y * 10);
            characterOrientation.ChangeOrientation(targetPoint);
        }
    }
}
