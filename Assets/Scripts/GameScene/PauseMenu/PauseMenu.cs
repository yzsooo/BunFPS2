using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public RectTransform PauseMenuPanel;

    private void Awake()
    {
        //TogglePauseMenu(false);
    }

    // set visibility of pause menu on and off
    public void TogglePauseMenu(bool bOn)
    {
        PauseMenuPanel.gameObject.SetActive(bOn);
    }

    // return to main scene
    public void QuitButton()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo("MainMenuScene");
    }
}
