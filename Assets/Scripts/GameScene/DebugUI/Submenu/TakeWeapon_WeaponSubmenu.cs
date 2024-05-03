using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon_WeaponSubmenu : DebugUISubmenuButton
{
    // remove the equipped weapon from the player
    public override void UseButton()
    {
        GameSceneManager.GameSceneManagerInstance.Player.attack.RemoveLoadedWeapon();
    }
}
