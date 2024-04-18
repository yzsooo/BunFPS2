using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameoverCamera gameoverCamera;
    // called by other gameobjects when the game is done
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
        // turn on gameover hud 
    }
}
