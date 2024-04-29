using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuSceneManager : MonoBehaviour
{
    [Header("Build name info")]
    public string buildName;
    public TextMeshProUGUI UIBuildText;

    private void Awake()
    {
        UIBuildText.text = "Build Name\n" + buildName;
    }
}
