using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public RectTransform PauseMenuPanel;
    public static bool bGamePaused = false;
    InputAction pause;

    private void Awake()
    {
        // bind pause button to togglepausemenu
        pause = new PlayerInput().GameControl.Escape;
        pause.performed += ctx => TogglePause(!bGamePaused);
        TogglePause(false);
    }

    // set visibility of pause menu on and off and pause/resume the timescale of the game
    public void TogglePause(bool bOn)
    {
        bGamePaused = bOn;
        PauseMenuPanel.gameObject.SetActive(bGamePaused);
        Time.timeScale = bOn ? 0.0f : 1.0f;
        // lock or unlock the mouse depending on the pause menu
        if (GameSceneManager.GameSceneManagerInstance)
        {
            // game paused = unlock mouse
            // game unpaused = lock mouse
            // so the arg to pass is reverse of bGamePaused
            GameSceneManager.GameSceneManagerInstance.Player.FlipMouseLock(false, !bGamePaused);
        }
        
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
