using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    
    public WeaponScriptableObject weaponStats;
    public Camera ViewmodelCamera;

    [Header("WeaponObject Scripts")]
    public WeaponObjectAnimationManager weaponAnimation;
    public WeaponObjectAttack attack;
    public WeaponObjectViewmodelSwayer ViewmodelSwayer;

    [Header("Scripts from Player")]
    public Camera cam;
    public PlayerAttack playerAttack;

    private void Awake()
    {
        AssignWeaponObjectComponents();
    }

    // assign variables and attach self to script variables
    void AssignWeaponObjectComponents()
    {
        weaponAnimation = GetComponent<WeaponObjectAnimationManager>();
        attack = GetComponent<WeaponObjectAttack>();
        ViewmodelCamera = GetComponent<Camera>();
        ViewmodelSwayer = GetComponent<WeaponObjectViewmodelSwayer>();

        weaponAnimation.weapon = this;
        attack.weapon = this;
    }

    // check if weapon is loaded by checking variables it needs to function
    public bool IsWeaponLoaded()
    {
        if (!cam || !playerAttack || !weaponStats || !ViewmodelCamera)
        {
            return false;
        }
        return true;
    }

    // run the attack function in this weapon's attack class and return if its a valid attack
    public bool StartAttack1()
    {
        bool attackIsValid = attack.StartAttack1();
        return attackIsValid;
    }

    // run the stop attack function in the weapon's attack class
    public void StopAttack1()
    {
        attack.StopAttack1();
    }

    // run the reload function in the weapon's attack class
    public void Reload()
    {
        attack.Reload();
    }
}
