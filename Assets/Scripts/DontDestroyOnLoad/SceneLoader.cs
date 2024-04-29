using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader SceneLoaderInstance { get; private set; }
    public RectTransform LoadingTransition;

    private void Awake()
    {
        SetSceneLoaderInstance();
    }
    // Set singleton
    void SetSceneLoaderInstance()
    {
        if (SceneLoaderInstance) { return; }
        SceneLoaderInstance = this;
    }

    // load new scene asyncrhonously and show loading screen
    public void ChangeSceneTo(string sceneName)
    {
        LoadingTransition.gameObject.SetActive(true);
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return null;
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);
        while(!loadAsync.isDone) { yield return null; }
        LoadingTransition.gameObject.SetActive(false);
        yield return null;
    }

    // reload the current scene
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
