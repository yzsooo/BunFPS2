using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatBehaviour : EnemyBehaviourState
{
    public float attackDuration = 1.0f;

    bool _bPlayerInRange;
    bool _bIsAttacking;

    public override void UpdateState()
    {
        if (!_bPlayerInRange && !_bIsAttacking)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Attack();
        }
    }

    // Trigger
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

    // Attacking
    void Attack()
    {
        // skip if already attacking
        if (_bIsAttacking) { return; }

        // stop pathfinding and attack with animation
        agent.ResetPath();
        _bIsAttacking= true;
        // start coroutine to reset attack bool
        StartCoroutine("ResetAttacking");
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
    }

    IEnumerator ResetAttacking()
    {
        // reset attack bool
        yield return new WaitForSeconds(attackDuration);
        _bIsAttacking = false;
        yield return null;

    }
}
