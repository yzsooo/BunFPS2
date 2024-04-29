using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptionsManager : MonoBehaviour
{
    public static PlayerOptionsManager PlayerOptionsInstance { get; private set; }

    [Header("Stage select options")]
    public WeaponScriptableObject selectedWeapon;
    public LevelInfoScriptableObject selectedLevel;

    private void Awake()
    {
        SetSingleton();
    }

    // set PlayerOptionsInstance singleton
    void SetSingleton()
    {
        if (PlayerOptionsInstance) { return; }
        PlayerOptionsInstance = this;
    }
    
    // called from StageSelect scene to save the selected weapon and level
    public void SaveStageSelectOptions(WeaponScriptableObject newWeapon, LevelInfoScriptableObject newLevel)
    {
        selectedWeapon = newWeapon;
        selectedLevel = newLevel;
    }
}
