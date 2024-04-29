using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    // Weapon
    [Header("Weapon")]
    public RectTransform weaponPanel;

    // Stage
    [Header("Stage")]
    public RectTransform stagePanel;

    // Selected stuff
    [Header("Selected Weapon and Stage")]
    public TextMeshProUGUI selectedWeaponText;
    public TextMeshProUGUI selectedStageText;
    WeaponScriptableObject _currentWeapon;
    LevelInfoScriptableObject _currentStage;

    private void Awake()
    {
        SetupWeaponAndStageSelection();
        SetupSelectedUIText();
    }

    // set parent of weapon and stage container ui to this
    void SetupWeaponAndStageSelection()
    {
        List<RectTransform> panels = new List<RectTransform> { weaponPanel, stagePanel };
        foreach (RectTransform panel in panels)
        {
            foreach (RectTransform container in panel)
            {
                SelectionContainerUI selectionContainer = container.GetComponent<SelectionContainerUI>();
                selectionContainer.parent = this;
            }
        }
    }

    // set selected weapon and stage text as blanks
    void SetupSelectedUIText()
    {
        selectedWeaponText.text = "";
        selectedStageText.text = "";
    }

    // update current selected weapon and text on ui
    public void SelectWeapon(WeaponScriptableObject weapon)
    {
        _currentWeapon = weapon;
        selectedWeaponText.text = _currentWeapon.weaponName;
    }

    // update current selected stage and text on ui
    public void SelectStage(LevelInfoScriptableObject level)
    {
        _currentStage = level;
        selectedStageText.text = _currentStage.LevelName;
    }

    // check if both weapon and stage is set
    // save into PlayerOptions and load into the selected stage
    public void ConfirmStageSelect()
    {
        if (_currentStage == null || _currentWeapon == null)
        {
            Debug.Log("Stage or weapon not selected");
            return;
        }
        // save selected weapons to PlayerOptions
        PlayerOptionsManager.PlayerOptionsInstance.SaveStageSelectOptions(_currentWeapon, _currentStage);
        // load selected stage
        SceneLoader.SceneLoaderInstance.ChangeSceneTo(_currentStage.SceneName);
    }
}
