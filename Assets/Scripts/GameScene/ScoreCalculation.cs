using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    [Header("Calculated scores")]
    public float scoreTime;
    public int scoreEnemyCount;
    public float scoreMissedEnemyPenaltyTime;
    public string scoreRank;

    // Calculate the score based on time and enemies left unkilled
    // based on the level's rank targets, determine which score the player achived; S, A, B or C
    public void CalculateScore(float goalTime, int remainingEnemies)
    {
        float baseTime;
        int enemyCount;
        string calcRank= "";

        baseTime = goalTime;
        enemyCount = remainingEnemies;

        LevelInfoScriptableObject levelinfo = GameSceneManager.GameSceneManagerInstance.LevelInfo;
        // Calculate score based on the base time and remaining enemies
        // total score is goal time + (number of remaining enemies * penalty per missed enemy)
        float penaltyTime = levelinfo.PentaltyPerMissedEnemy * remainingEnemies;
        Debug.Log("Missed enemies: " + remainingEnemies + ", penalty: " + penaltyTime);
        float totalTime = baseTime + penaltyTime;
        // see which rank it achieved
        Dictionary<float, string> ranks = new Dictionary<float, string>
        {
            {levelinfo.ScoreTarget_RankS, "S" },
            {levelinfo.ScoreTarget_RankA, "A" },
            {levelinfo.ScoreTarget_RankB, "B" },

        };

        foreach (KeyValuePair<float, string>kvp in ranks)
        {
            if (totalTime <= kvp.Key)
            {
                calcRank = kvp.Value;
                break;
            }
            // if total time is outside S, A or B range then rank is C
            calcRank = "C";
        }
        Debug.Log("Player score is "+ totalTime + ", rank is " + calcRank);
        // update the calculated score
        scoreTime = totalTime;
        scoreEnemyCount = remainingEnemies;
        scoreMissedEnemyPenaltyTime = penaltyTime;
        scoreRank = calcRank;
    }
}
