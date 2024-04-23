using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGoalGateManager : MonoBehaviour
{
    public LevelTimer levelTimer;

    StartGoalGate _startGate;
    StartGoalGate _goalGate;
    bool _bPlayerEntered = false;

    private void Awake()
    {
        SetStartGoalGates();
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
        // mark player entering gate depending on the gate type
        if (!_bPlayerEntered && gateType == StartGoalGate.GateType.Start)
        {
            _bPlayerEntered = true;
            levelTimer.timerActive = true;
            Debug.Log("Player entered");
        }
        if (_bPlayerEntered && gateType == StartGoalGate.GateType.Goal)
        {
            _bPlayerEntered = false;
            levelTimer.timerActive = false;
            Debug.Log("Player reached goal");
        }
    }
}
