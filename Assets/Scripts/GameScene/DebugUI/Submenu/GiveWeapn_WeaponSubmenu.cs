using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWeapn_WeaponSubmenu : DebugUISubmenuButton
{
    [SerializeField]
    WeaponObject _weaponToGive;

    // Load the given weapon object to the player
    public override void UseButton()
    {
        GameSceneManager.GameSceneManagerInstance.Player.attack.LoadWeapon(_weaponToGive);
    }
}
