using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Animator weaponAnimator;

    public void Start()
    {
        if(weaponAnimator == null)
        {
            weaponAnimator = GetComponentInChildren<Animator>();
        }
    }

    public void PrimaryAttack()
    {
        weaponAnimator.SetTrigger("Attack");
    }
}
