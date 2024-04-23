using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StartGoalGate : MonoBehaviour
{
    public StartGoalGateManager parent;
    public enum GateType
    {
        Start,
        Goal
    }

    public GateType thisGateType = GateType.Start;


    private void OnTriggerEnter(Collider other)
    {
        parent.PlayerEnteredGate(thisGateType);
    }
}
