using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGoalGateManager : MonoBehaviour
{
    public LevelTimer levelTimer;

    StartGoalGate _startGate;
    StartGoalGate _goalGate;
    bool _bPlayerEntered = false;

    public Transform enemyCollection;
    int _enemyCount = 0;

    private void Awake()
    {
        SetStartGoalGates();
        _enemyCount = CountEnemy();
        SetEnemyCollectionActive(false);
    }
    int CountEnemy()
    {
        int count = 0;
        foreach (Transform t in enemyCollection)
        {
            if (t.GetComponent<Entity>() != null)
            {
                count += 1;
            }
        }
        return count;
    }

    void SetStartGoalGates()
    {
        foreach (Transform t in transform)
        {
            StartGoalGate gate = t.GetComponent<StartGoalGate>();
            switch(gate.thisGateType)
            {
                case StartGoalGate.GateType.Start:
                    _startGate = gate;
                    break;
                case StartGoalGate.GateType.Goal:
                    _goalGate = gate;
                    break;
            }
            gate.parent = this;
        }
    }

    public void PlayerEnteredGate(StartGoalGate.GateType gateType)
    {
        // Start gate
        // reset timer and activate enemies
        if (!_bPlayerEntered && gateType == StartGoalGate.GateType.Start)
        {
            Debug.Log("Player start");
            _bPlayerEntered = true;
            levelTimer.currentTime = 0.0f;
            levelTimer.timerActive = true;
            SetEnemyCollectionActive(true);
        }

        // Goal gate
        // turn off timer, deactivate enemies and count how many are still alive
        if (_bPlayerEntered && gateType == StartGoalGate.GateType.Goal)
        {

            Debug.Log("Player reached goal");
            _bPlayerEntered = false;
            levelTimer.timerActive = false;
            SetEnemyCollectionActive(false);
            Debug.Log("Remaining enemies; " + CountEnemy().ToString());
            GameSceneManager.GameSceneManagerInstance.ScoreCalculation.CalculateScore(levelTimer.currentTime, CountEnemy());
        }
    }

    void SetEnemyCollectionActive(bool bActive)
    {
        foreach (Transform t in enemyCollection)
        {
            t.gameObject.SetActive(bActive);
        }
    }
}
