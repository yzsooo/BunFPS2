using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    
    public WeaponScriptableObject weaponStats;

    // scripts in weaponObject
    WeaponObjectAnimationManager _animation;
    WeaponObjectAttack _attack;

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
        _animation = GetComponent<WeaponObjectAnimationManager>();
        _attack = GetComponent<WeaponObjectAttack>();

        // assign self to each script variables
        _animation.weapon = this;
        _attack.weapon = this;
    }

    public bool StartAttack1()
    {
        bool attackIsValid = _attack.StartAttack1();
        return attackIsValid;
    }

    public void StopAttack1()
    {
        _attack.StopAttack1();
    }

}
