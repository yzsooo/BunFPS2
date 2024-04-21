using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviourState : MonoBehaviour
{
    public EnemyBehaviour.MovementPattern thisState;

    public EnemyBehaviour parent;
    public NavMeshAgent agent;
    public PlayerManager player;
    public Animator anim;

    public virtual void UpdateState()
    {

    }
}
