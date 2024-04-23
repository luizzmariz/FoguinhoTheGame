using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine;

    [Header("Primary")]
    public GameObject primaryAttack;
    public Animator primaryWeaponAnimator;
    public float primaryAttackDamage;

    [Header("Secondary")]
    public GameObject secondaryAttack;
    public float secondaryAttackDamage;
    public float secondaryAttackVelocity;
    public float secondaryAttackTimeOfLife;

    [Header("Trap")]
    public GameObject trap;
    public float trapDamage;
    public float trapTimeOfLife;

    public void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    public void PrimaryAttack()
    {
        primaryAttack.GetComponent<MeleeWeapon>().damageAmount = primaryAttackDamage;
        primaryWeaponAnimator.SetTrigger("Attack");
    }

    public void SecondaryAttack(Vector3 orientation)
    {
        Quaternion LookAtRotation = Quaternion.LookRotation(orientation);
        Quaternion projectileRotation = Quaternion.Euler(secondaryAttack.transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, secondaryAttack.transform.rotation.eulerAngles.z);

        GameObject attack = Instantiate(secondaryAttack, transform.position, projectileRotation);

        attack.GetComponent<RangedWeapon>().damageAmount = secondaryAttackDamage;
        attack.GetComponent<RangedWeapon>().velocity = orientation.normalized * secondaryAttackVelocity;
        attack.GetComponent<RangedWeapon>().timeOfLife = secondaryAttackTimeOfLife;

        playerStateMachine.CastAttackEnded();
    }

    public void PlaceTrap(Vector3 targetPosition, Vector3 playerPosition)
    {
        GameObject placedTrap = Instantiate(trap, transform.position, trap.transform.rotation);
        placedTrap.GetComponent<Trap>().damageAmount = trapDamage;
        placedTrap.GetComponent<Trap>().timeOfLife = trapTimeOfLife;
        playerStateMachine.CastAttackEnded();
    }
}
