using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CurrentSelectionDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI weaponDesc;
    public TextMeshProUGUI levelDesc;

    private void Awake()
    {
        // disable weapon and level description
        weaponDesc.gameObject.SetActive(false);
        levelDesc.gameObject.SetActive(false);
    }

    // update the weapon description text to the selected weapon
    public void UpdateWeaponDesc(WeaponScriptableObject weapon)
    {
        weaponDesc.gameObject.SetActive(true);
        weaponDesc.text = weapon.weaponName + "\n" + weapon.weaponDescription;
    }

    // update the level description to show the best time/score (WIP because theres no saving feature)
    // update the level description
    public void UpdateLevelDesc(LevelInfoScriptableObject level)
    {
        levelDesc.gameObject.SetActive(true);
        levelDesc.text = level.LevelName;
    }
}
