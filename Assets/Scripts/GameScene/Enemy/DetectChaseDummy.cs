using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectChaseDummy : MonoBehaviour
{

    public enum MovementPattern
    {
        Idle,
        Combat
    }

    [SerializeField]
    MovementPattern _currentMovementPattern = MovementPattern.Idle;

    public DetectionTrigger detectionTrigger;

    PlayerManager _detectedPlayer;
    NavMeshAgent agent;
    Animator anim;

    [SerializeField]
    DetecChaseEnemyCombat combat;

    private void Awake()
    {
        detectionTrigger.parent = this;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        combat.agent = agent;
        combat.anim = anim;
    }

    private void FixedUpdate()
    {
        ProcessState();
    }

    void ProcessState()
    {
        switch (_currentMovementPattern)
        {
            case MovementPattern.Idle:
                {
                    break;
                }
            case MovementPattern.Combat:
                {
                    UpdateCombatState();
                    break;
                }
        }
    }

    void UpdateCombatState()
    {
        //agent.SetDestination(_detectedPlayer.transform.position);
        combat.UpdateState();
    }

    public void DetectionTriggered(PlayerManager player)
    {
        _detectedPlayer = player;
        _currentMovementPattern = MovementPattern.Combat;
        combat.player = player;
    }
}
