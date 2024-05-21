using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGoalGate : MonoBehaviour
{
    public StartGoalGateManager parent;

    // gate can be either start gate or goal gate
    public enum GateType
    {
        Start,
        Goal
    }

    public GateType thisGateType = GateType.Start;

    // call GateManager's gate enter function
    private void OnTriggerEnter(Collider other)
    {
        parent.PlayerEnteredGate(thisGateType);
    }
}
