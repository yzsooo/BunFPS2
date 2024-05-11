using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    public RectTransform gameoverUIPanel;

    public TextMeshProUGUI gameoverUIHeader;

    [Header("Statistics text field")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI missedEnemiesText;
    public TextMeshProUGUI missedEnemiesPenaltyText;
    public TextMeshProUGUI rankText;

    private void Awake()
    {
        gameoverUIPanel.gameObject.SetActive(false);
    }

    // load the calculated score to the UI, and change the text on the UI based on player win/loss
    public void EnableGameoverUI(bool bWin)
    {
        gameoverUIPanel.gameObject.SetActive(true);
        // unlock mouse so that the gameoverui can be controlled
        GameSceneManager.GameSceneManagerInstance.Player.FlipMouseLock(false, false);

        LoadCalculatedScoreToUI();
        // Win
        if (bWin)
        {
            gameoverUIHeader.text = "Level Clear!";
        }
        // Lose
        else if (!bWin)
        {
            gameoverUIHeader.text = "Gameover!";
            rankText.text = "N/A";
        }

    }

    // load the time, missed enemies, penalty and final score into UI
    void LoadCalculatedScoreToUI()
    {
        ScoreCalculation sc = GameSceneManager.GameSceneManagerInstance.ScoreCalculation;
        timeText.text = sc.scoreTime.ToString("0.00");
        missedEnemiesText.text = sc.scoreEnemyCount.ToString();
        // if theres any missed enemies, show the penalty time
        // otherwise make the text empty
        if (sc.scoreEnemyCount > 0)
        {
            missedEnemiesPenaltyText.text = "+" + sc.scoreMissedEnemyPenaltyTime.ToString("0.00") + " seconds";
        }
        else
        {
            missedEnemiesPenaltyText.text = "";
        }
        rankText.text = sc.scoreRank;
    }

    // restart the scene
    public void RetryButton()
    {
        SceneLoader.SceneLoaderInstance.RestartScene();
    }

    // quit to main menu
    public void QuitButton()
    {
        SceneLoader.SceneLoaderInstance.ChangeSceneTo("MainMenuScene");
    }
}
