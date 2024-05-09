using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public RectTransform PauseMenuPanel;
    public static bool bGamePaused = false;
    public InputAction pause;

    private void Awake()
    {
        // bind pause button to togglepausemenu
        pause = new PlayerInput().GameControl.Escape;
        pause.performed += ctx => TogglePauseMenu(!bGamePaused);
        TogglePauseMenu(false);
    }

    // set visibility of pause menu on and off
    public void TogglePauseMenu(bool bOn)
    {
        bGamePaused = bOn;
        PauseMenuPanel.gameObject.SetActive(bGamePaused);
    }

    // return to main scene
    public void QuitButton()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo("MainMenuScene");
    }

    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }
}
