using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShooterCombatBehaviour : EnemyBehaviourState
{
    [SerializeField]
    bool _bPlayerInSight;
    [SerializeField]

    bool _bIsAttacking;

    enum ShooterState
    {
        Attack,
        Wander
    }

    [SerializeField]
    ShooterState _currentState = ShooterState.Attack;

    [Header("Ranged Attack properties")]
    public Transform projectile;
    public float projectileSpeed;
    public float projectileDamage;
    public float attackDuration = 1.0f;
    public Transform projectileSpawnPosition;
    public float reattackChance = 0.5f;

    [Header("Wander properties")]
    public float wanderRange;
    [SerializeField]

    bool _bIsWandering;
    Vector3 wanderPosition;

    // Process attack state
    public override void UpdateState()
    {
        switch (_currentState)
        {
            case ShooterState.Attack:
                Attack();
                break;
            case ShooterState.Wander:
                Wander();
                break;
        }
    }

    // Attack
    // Move towards the player until its in line of sight then shoot
    void Attack()
    {
        LookAtPlayerInSight();
        FireProjectile();
    }

    // Look at the player if player is in range or
    // move towards the player until it can
    void LookAtPlayerInSight()
    {
        // shoot raycast to see if player is in direct line of sight
        Vector3 toPlayer = Vector3.Normalize(player.transform.position - transform.position);
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        bool ray = Physics.Raycast(transform.position, toPlayer, out hit, Mathf.Infinity, layerMask);
        bool raycastValid = (ray && hit.collider.GetComponent<EntityColliderInfo>() != null);

        _bPlayerInSight = raycastValid;

        // if player is not in line of sight then move towards player until it can
        if (!_bPlayerInSight)
        {
            agent.SetDestination(player.transform.position);
            return;
        }
        // look at player 
        parent.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    // Shoot a projectile towards the player with the set parameters
    void FireProjectile()
    {
        // if the player is not in sight or the player is already attacking then dont do anything
        if (!_bPlayerInSight || _bIsAttacking) { return; }
        _bIsAttacking = true;
        Transform proj = Instantiate(projectile, projectileSpawnPosition.position, projectileSpawnPosition.transform.rotation);
        Vector3 toPlayer = Vector3.Normalize(player.transform.position - transform.position);
        proj.GetComponent<EnemyProjectile>().SpawnProjectile(projectileSpeed, projectileDamage, toPlayer);
        _bIsAttacking = true;
        // start coroutine to reset attack bool
        StartCoroutine(ResetAttacking(true));
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
    }

    // Wait for attackDuration seconds and
    // either attack again or wander to a new position
    IEnumerator ResetAttacking(bool bChanceToWander = false)
    {
        // reset attack bool
        yield return new WaitForSeconds(attackDuration);

        if (bChanceToWander)
        {
            if (Random.value < reattackChance)
            {
                _bPlayerInSight = false;
                _currentState = ShooterState.Wander;
            }
            else
            {
                _bIsAttacking = false;
            }
        }

        yield return null;
    }

    // Wander
    // relocate to a random position within range, then set state to Attack
    void Wander()
    {
        // Set random position to wander to
        if (!_bIsWandering)
        {
            SetWanderPosition();
            // because theres a delay to calculate the path, check agent has a path before setting iswandering to true
            if (agent.hasPath) { _bIsWandering = true; }
        }
        // Go back to Attack state after reaching to the wander destination
        if (_bIsWandering)
        {
            if (!agent.hasPath)
            {
                _bIsWandering = false;
                _bIsAttacking = false;
                _currentState = ShooterState.Attack;
            }
        }
    }

    // Get a random position within wanderRange and set it as the pathfinding destination if it can get there
     void SetWanderPosition()
    {
        NavMeshPath path = new NavMeshPath();
        bool bPathValid = false;
        while (!bPathValid)
        {
            Vector3 wanderOffset = Random.insideUnitSphere * wanderRange;
            wanderOffset.y = 0;
            wanderPosition = transform.position + wanderOffset;
            bPathValid = agent.CalculatePath(wanderPosition, path);
        }
        agent.SetDestination(wanderPosition);
    }
}
