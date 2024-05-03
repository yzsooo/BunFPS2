using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatBehaviour : EnemyBehaviourState
{
    public float attackDuration = 1.0f;

    bool _bPlayerInRange;
    bool _bIsAttacking;

    // Move to the player until its in melee range, then attack
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

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerSetPlayerInRange(other.GetComponent<PlayerManager>());
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerSetPlayerInRange(other.GetComponent<PlayerManager>(), true);
    }

    // Called by OnTriggerEnter and OnTriggerExit
    // Check if the collider is a player and set bPlayerInRange;
    // bExit is for TriggerExit
    void OnTriggerSetPlayerInRange(PlayerManager pm, bool bExit = false)
    {
        if (pm == null) { return; }
        _bPlayerInRange = !bExit;
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

    // Wait attackDuration seconds and allow enemy to attack again
    IEnumerator ResetAttacking()
    {
        yield return new WaitForSeconds(attackDuration);
        _bIsAttacking = false;
        yield return null;

    }
}
