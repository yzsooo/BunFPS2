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

    public GameoverUI gameoverUI;

    private void Awake()
    {
        SetStartGoalGates();
        _enemyCount = CountEnemy();
        SetEnemyCollectionActive(false);
    }

    // count how many enemies are in enemyCollection
    public int CountEnemy()
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

    // set start and goal gate variables and set their parent var
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

    // called from the gates, either start the level timer or end it
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
            // update the time, remaining enemies and rank
            GameSceneManager.GameSceneManagerInstance.ScoreCalculation.CalculateScore(levelTimer.currentTime, CountEnemy());
            // call gameoverui's win function, if the player reached the goal without dying itll count as a win
            GameSceneManager.GameSceneManagerInstance.GameoverManager.gameoverUI.EnableGameoverUI(true);
        }
    }

    // toggle enemies in enemyCollection active or inactive
    void SetEnemyCollectionActive(bool bActive)
    {
        foreach (Transform t in enemyCollection)
        {
            t.gameObject.SetActive(bActive);
        }
    }
}
