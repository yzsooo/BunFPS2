using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeapn_WeaponSubmenu : DebugUISubmenuButton
{
    [SerializeField]
    WeaponObject _weaponToGive;
    public override void UseButton()
    {
        Debug.Log("Bingus");
        GameSceneManager.GameSceneManagerInstance.Player.attack.LoadWeapon(_weaponToGive);
    }
}
