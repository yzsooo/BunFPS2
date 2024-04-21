using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DetecChaseEnemyCombat : MonoBehaviour
{
    public NavMeshAgent agent;
    public PlayerManager player;
    public Animator anim;

    public float attackDuration = 1.0f;

    [SerializeField]
    bool _bPlayerInRange;
    bool _bIsAttacking;

    public void UpdateState()
    {
        // Chase the player if the player is not in range and its already not attacking
        if (!_bPlayerInRange && !_bIsAttacking)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        _bPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }
        _bPlayerInRange = false;
    }

    void Attack()
    {
        // skip if its already attacking
        if (_bIsAttacking) { return; }

        // attack by stopping pathfinding to do the attack animation
        agent.ResetPath();
        _bIsAttacking = true;
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
        StartCoroutine("ResetAttacking");
    }

    IEnumerator ResetAttacking()
    {
        // reset attack bool
        yield return new WaitForSeconds(attackDuration);
        _bIsAttacking = false;
        yield return null;
    }

}
