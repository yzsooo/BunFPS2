using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    // Weapon
    [Header("Weapon")]
    public RectTransform weaponSelection;
    public CurrentSelectionUI currentWeaponSelection;

    // Stage
    [Header("Stage")]
    public RectTransform stageSelection;
    public CurrentSelectionUI currentStageSelection;

    [Header("")]
    public CurrentSelectionDescriptionUI selectionDescription;

    // Currently selected stuff
    WeaponScriptableObject _currentWeapon;
    LevelInfoScriptableObject _currentLevel;

    private void Awake()
    {
        SetupWeaponStageSelection();
    }

    // Set parent variable of each container in weapon and level selection menu
    void SetupWeaponStageSelection()
    {
        List<RectTransform> panels = new List<RectTransform> { weaponSelection, stageSelection };
        foreach (RectTransform panel in panels)
        {
            foreach (RectTransform container in panel)
            {
                container.GetComponent<SelectionContainerUI>().parent = this;
            }
        }
    }

    // update the currently selected weapon and update the UI description alongisde it
    public void SelectWeapon(WeaponScriptableObject weapon)
    {
        _currentWeapon = weapon;
        currentWeaponSelection.UpdateCurrentSelection(_currentWeapon.name, _currentWeapon.weaponIcon);
        currentWeaponSelection.ToggleSelection(false);
        selectionDescription.UpdateWeaponDesc(weapon);
    }

    // update the currently selected stage and update the UI description alongisde it
    public void SelectStage(LevelInfoScriptableObject level)
    {
        _currentLevel = level;
        currentStageSelection.UpdateCurrentSelection(_currentLevel.name, _currentLevel.LevelIcon);
        currentStageSelection.ToggleSelection(false);
        selectionDescription.UpdateLevelDesc(level);
    }

    // check if both weapon and stage is set
    // save into PlayerOptions and load into the selected stage
    public void ConfirmStageSelect()
    {
        if (_currentLevel == null || _currentWeapon == null)
        {
            Debug.Log("Stage or weapon not selected");
            return;
        }
        // save selected weapons to PlayerOptions
        PlayerOptionsManager.PlayerOptionsInstance.SaveStageSelectOptions(_currentWeapon, _currentLevel);
        // load selected stage
        SceneLoader.SceneLoaderInstance.ChangeSceneTo(_currentLevel.SceneName);
    }

    // load back to the main scene
    public void ReturnToMainScene()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo("MainMenuScene");
    }
}
