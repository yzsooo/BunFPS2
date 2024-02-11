using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon_WeaponSubmenu : DebugUISubmenuButton
{
    public override void UseButton()
    {
        GameSceneManager.GameSceneManagerInstance.Player.attack.RemoveLoadedWeapon();
    }
}
