using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Animator primaryWeaponAnimator;
    public GameObject secondaryAttack;
    public GameObject trap;
    public PlayerStateMachine playerStateMachine;

    public void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    public void PrimaryAttack()
    {
        primaryWeaponAnimator.SetTrigger("Attack");
    }

    public void SecondaryAttack(Vector3 orientation)
    {
        // Instantiate(secondaryAttack, transform.position, transform.rotation);
        GameObject attack = Instantiate(secondaryAttack, transform.position, Quaternion.identity);
        attack.GetComponent<RangedWeapon>().velocity = orientation.normalized * 8;
        attack.GetComponent<RangedWeapon>().timeOfLife = 3;
        playerStateMachine.CastAttackEnded();
    }

    public void PlaceTrap(Vector3 targetPosition, Vector3 playerPosition)
    {
        GameObject placedTrap = Instantiate(trap, transform.position, Quaternion.identity);
        // attack.GetComponent<RangedWeapon>().velocity = orientation.normalized * 8;
        placedTrap.GetComponent<Trap>().timeOfLife = 3;
        // playerStateMachine.CastAttackEnded();
    }
}
