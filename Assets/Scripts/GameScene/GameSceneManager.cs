using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
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
    }

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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
