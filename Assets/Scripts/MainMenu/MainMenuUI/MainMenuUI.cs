using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Selcetion Image")]
    public Image SelectionImage;
    public Sprite StartSelectionIcon;
    public Sprite QuitSelectionIcon;
    // call the scene loader and change to the stage select scene
    public void StageSelect()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo("StageSelectScene");
    }

    // quit the game 
    public void QuitButton()
    {
        Application.Quit();
    }

    // when the mouse cursor hovers over the button, change the selection image to the appropriate button icon
    public void MenuButtonHover(string buttonName)
    {
        // dictionary to match name and sprite
        Dictionary<string, Sprite> selectionIcon = new Dictionary<string, Sprite>
        {
            {"start", StartSelectionIcon },
            {"quit", QuitSelectionIcon }
        };

        if (selectionIcon[buttonName])
        {
            SelectionImage.sprite = selectionIcon[buttonName];
            SelectionImage.color = Color.white;
        }
    }
}