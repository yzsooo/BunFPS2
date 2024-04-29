using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public SceneAsset stageSelectScene;

    public void StageSelect()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo(stageSelectScene.name);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}