using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public SceneAsset stageSelectScene;

    // call the scene loader and change to the stage select scene
    public void StageSelect()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo(stageSelectScene.name);
    }

    // quit the game 
    public void QuitButton()
    {
        Application.Quit();
    }
}