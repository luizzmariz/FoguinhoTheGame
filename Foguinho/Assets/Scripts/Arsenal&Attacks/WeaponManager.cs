using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Animator primaryWeaponAnimator;
    public GameObject secondaryAttack;
    public PlayerStateMachine playerStateMachine;

    public void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
    }

    public void PrimaryAttack()
    {
        primaryWeaponAnimator.SetTrigger("Attack");
    }

    public void SecondaryAttack()
    {
        Instantiate(secondaryAttack, transform.position, transform.rotation);
        playerStateMachine.CastAttackEnded();
    }
}
