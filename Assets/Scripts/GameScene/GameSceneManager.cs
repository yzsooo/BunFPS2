using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // this script is set to execute last so a lot of game start/game over and state transition stuff should be done here

    public LevelInfoScriptableObject LevelInfo;
    public static GameSceneManager GameSceneManagerInstance { get; private set; }

    public GameOverManager GameoverManager;
    public ScoreCalculation ScoreCalculation;
    [Header("Critical Components")]
    public PlayerManager Player;

    private void Awake()
    {
        SetGameSceneManagerInstance();
        GameoverManager = GetComponent<GameOverManager>();
        ScoreCalculation = GetComponent<ScoreCalculation>();
        LoadPlayerOptions();
        Player.HUD.ChangeHUDMode(PlayerHUDManager.PlayerHUDMode.Full);
    }

    IEnumerator testload()
    {
        yield return new WaitForSeconds(5.0f);
        LoadPlayerOptions();
        yield return null;
    }

    // Give player the selected weapon in PlayerOptions
    public void LoadPlayerOptions()
    {
        Debug.Log("Load player options");
        Debug.Log(PlayerOptionsManager.PlayerOptionsInstance.selectedWeapon.weaponName);
        if (!PlayerOptionsManager.PlayerOptionsInstance) { return; }
        Player.attack.LoadWeapon(PlayerOptionsManager.PlayerOptionsInstance.selectedWeapon.weaponPrefab);
    }

    // Set GameSceneManager singleton
    void SetGameSceneManagerInstance()
    {
        // Check if GSMI already exists and do nothing if it is
        if (GameSceneManagerInstance != null)
        {
            Debug.Log("GameSceneManagerInstance already exists");
            return;
        }
        // set GMSI to this object
        GameSceneManagerInstance = this;
        Debug.Log("GameSceneManagerInstance set");
    }
}
