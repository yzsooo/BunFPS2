using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourState : MonoBehaviour
{
    // Set which movementpattern this state is for
    public EnemyBehaviour.MovementPattern thisState;

    public EnemyBehaviour parent;
    public NavMeshAgent agent;
    public PlayerManager player;
    public Animator anim;

    // virtual function to run every frame to process this state
    public virtual void UpdateState()
    {

    }
}
