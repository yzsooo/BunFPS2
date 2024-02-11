using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager GameSceneManagerInstance { get; private set; }

    [Header("Critical Components")]
    public PlayerManager Player;

    private void Awake()
    {
        SetGameSceneManagerInstance();
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
}
