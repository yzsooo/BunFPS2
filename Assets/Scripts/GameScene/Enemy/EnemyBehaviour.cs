using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Enemy movement patterns
    // Idle: enemy doesnt do anything
    // Combat: enemy is actively chasing the player and fighting them
    public enum MovementPattern
    {
        Idle,
        Combat
    }

    [SerializeField]
    MovementPattern _currentMovementPattern = MovementPattern.Idle;

    public DetectionTrigger detection;
    public EnemyBehaviourState combat;
    public EnemyBehaviourState idle;
    [SerializeField]
    EnemyBehaviourState currentState;

    NavMeshAgent agent;
    Animator anim;

    private void Awake()
    {
        // set variables
        detection.parent = this;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // set variables on states
        List<EnemyBehaviourState> states = new List<EnemyBehaviourState>(){ combat, idle };

        foreach (EnemyBehaviourState state in states)
        {
            if (state == null) { return; }
            state.parent = this;
            state.agent = agent;
            state.anim = anim;
        }
        // change state to the initialized movement pattern (Idle by default)
        SwitchState(_currentMovementPattern);
    }

    private void FixedUpdate()
    {
        ProcessState();
    }

    // call the UpdateState function on the current state
    void ProcessState()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    // switch to the selected movementpattern
    void SwitchState(MovementPattern switchTo)
    {
        _currentMovementPattern = switchTo;
        // could use a dict for this
        switch(switchTo)
        {
            case MovementPattern.Idle:
            {
                currentState = idle;
                break;
            }
            case MovementPattern.Combat:
                {
                    currentState = combat;
                    break;
                }
        }
    }

    // Called by the detectionTrigger child object
    // change state to Combat and set the detected player object in the combat state
    public void DetectionTriggered(PlayerManager player)
    {
        SwitchState(MovementPattern.Combat);
        combat.player = player;
    }
}
