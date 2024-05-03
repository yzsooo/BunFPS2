using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameOverManager : MonoBehaviour
{
    public GameoverCamera gameoverCamera;

    //[SerializeField]
    //bool bGameStarted = false;

    //// called by GameSceneManager when the game starts
    //// disable player input and hud until theres a mouse input
    //public void GamePrestart()
    //{
    //    PlayerManager player = GameSceneManager.GameSceneManagerInstance.Player;
    //    player.HUD.ChangeHUDMode(PlayerHUDManager.PlayerHUDMode.MessageOnly);
    //    player.HUD.ShowMessage("Press LEFT CLICK to start");
    //    player.PlayerEnabled = false;
    //    Debug.Log("Game ready to start");
    //}

    //public void GameStart()
    //{
    //    if (bGameStarted) { return; }
    //    PlayerManager player = GameSceneManager.GameSceneManagerInstance.Player;
    //    player.PlayerEnabled = true;
    //    player.HUD.ChangeHUDMode(PlayerHUDManager.PlayerHUDMode.Full);
    //    player.HUD.ShowMessage("");
    //    bGameStarted = true;
    //    GameSceneManager.GameSceneManagerInstance.LoadPlayerOptions();
    //    Debug.Log("Game started");
    //}

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
    }
}
