using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainerUI : SelectionContainerUI
{
    public WeaponScriptableObject weaponInfo;

    // set the name of this container text to the weapon's name
    public override void SetContainerUI()
    {
        selectionName.text = weaponInfo.weaponName;
    }

    // set this weapon as the selected weapon
    public override void SelectThisContainer()
    {
        parent.SelectWeapon(weaponInfo);
    }
}
