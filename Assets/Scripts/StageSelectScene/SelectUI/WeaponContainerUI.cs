using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainerUI : SelectionContainerUI
{
    public WeaponScriptableObject weaponInfo;

    public override void SetContainerUI()
    {
        selectionName.text = weaponInfo.weaponName;
    }

    public override void SelectThisContainer()
    {
        parent.SelectWeapon(weaponInfo);
    }
}
