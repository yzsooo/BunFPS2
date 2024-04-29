using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelInfo", menuName = "LevelInfo Scriptable Object")]
public class LevelInfoScriptableObject : ScriptableObject
{
    public string LevelName;
    public string SceneName;

    [Header("Scoring")]
    public float PentaltyPerMissedEnemy = 1.0f;
    public float ScoreTarget_RankS = 0.0f;
    public float ScoreTarget_RankA = 0.0f;
    public float ScoreTarget_RankB = 0.0f;

}
