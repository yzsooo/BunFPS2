using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    
    public WeaponScriptableObject weaponStats;
    public Camera ViewmodelCamera;

    // scripts in weaponObject
    public WeaponObjectAnimationManager weaponAnimation;
    public WeaponObjectAttack attack;

    // scripts from Player
    public Camera cam;
    public PlayerAttack playerAttack;

    private void Awake()
    {
        AssignWeaponObjectComponents();
    }

    void AssignWeaponObjectComponents()
    {
        // assign variables
        weaponAnimation = GetComponent<WeaponObjectAnimationManager>();
        attack = GetComponent<WeaponObjectAttack>();
        ViewmodelCamera = GetComponent<Camera>();

        // assign self to each script variables
        weaponAnimation.weapon = this;
        attack.weapon = this;
    }

    public bool StartAttack1()
    {
        bool attackIsValid = attack.StartAttack1();
        return attackIsValid;
    }

    public void StopAttack1()
    {
        attack.StopAttack1();
    }

}
