using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameOverManager : MonoBehaviour
{
    public GameoverCamera gameoverCamera;
    public GameoverUI gameoverUI;

    // called by other gameobjects when the game is done
    // Disable player controls, switch to gameover thirdperson camera and switch to gameover hud
    public void GameOver()
    {
        Debug.Log("Gameover called");
        // take control away from player
        GameSceneManager.GameSceneManagerInstance.Player.PlayerEnabled = false;
        // switch on gameover cam
        gameoverCamera.transform.position = GameSceneManager.GameSceneManagerInstance.Player.transform.position;
        gameoverCamera.gameObject.SetActive(true);
        GameSceneManager.GameSceneManagerInstance.Player.playerCamera.enabled = false;
        // turn off player hud
        GameSceneManager.GameSceneManagerInstance.Player.HUD.ChangeHUDMode(PlayerHUDManager.PlayerHUDMode.Disabled);
        // turn on gameover hud
        gameoverUI.EnableGameoverUI(false);
    }
}
