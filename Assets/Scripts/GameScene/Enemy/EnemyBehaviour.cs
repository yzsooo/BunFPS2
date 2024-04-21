using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
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
        detection.parent = this;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // set vars on states
        List<EnemyBehaviourState> states = new List<EnemyBehaviourState>(){ combat, idle };

        foreach (EnemyBehaviourState state in states)
        {
            if (state == null) { return; }
            state.parent = this;
            state.agent = agent;
            state.anim = anim;
        }

        SwitchState(_currentMovementPattern);
    }

    private void FixedUpdate()
    {
        ProcessState();
    }

    void ProcessState()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    void SwitchState(MovementPattern switchTo)
    {
        _currentMovementPattern = switchTo;
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

    public void DetectionTriggered(PlayerManager player)
    {
        SwitchState(MovementPattern.Combat);
        combat.player = player;
    }
}
