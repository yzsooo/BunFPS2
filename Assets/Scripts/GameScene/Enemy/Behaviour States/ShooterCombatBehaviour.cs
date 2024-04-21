using System.Collections;
using System.Collections.Generic;
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

    void Attack()
    {
        LookAtPlayerInSight();
        FireProjectile();
    }

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

        // if player is not in line of sight then end
        if (!_bPlayerInSight) { return; }
        // look at player 
        parent.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    void FireProjectile()
    {
        // if the player is not in sight or the player is already attacking then dont do anything
        if (!_bPlayerInSight || _bIsAttacking) { return; }
        _bIsAttacking = true;
        Transform proj = Instantiate(projectile, projectileSpawnPosition.position, projectileSpawnPosition.transform.rotation);
        proj.GetComponent<EnemyProjectile>().SpawnProjectile(projectileSpeed, projectileDamage, transform.forward.normalized);
        _bIsAttacking = true;
        // start coroutine to reset attack bool
        StartCoroutine(ResetAttacking(true));
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
    }

    IEnumerator ResetAttacking(bool bChanceToWander = false)
    {
        // reset attack bool
        yield return new WaitForSeconds(attackDuration);

        if (bChanceToWander)
        {
            if (Random.value < reattackChance)
            {
                _currentState = ShooterState.Wander;
            }
            else
            {
                Debug.Log("repeating attack");
                _bIsAttacking = false;
            }
        }

        yield return null;
    }

    void Wander()
    {
        if (!_bIsWandering)
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
            // because theres a delay to calculate the path, check agent has a path before setting iswandering to true
            if (agent.hasPath) { _bIsWandering = true; }
        }
        if (_bIsWandering)
        {
            Debug.Log(agent.hasPath);
            if (!agent.hasPath)
            {
                _bIsWandering = false;
                _bIsAttacking = false;
                _currentState = ShooterState.Attack;
            }
        }
        //Debug.Log(agent.hasPath);
        //if (_bIsWandering && !agent.hasPath)
        //{
        //    Debug.Log("Reached target");
        //}
    }
    //void Wander()
    //{
    //    // set wander destination if not set
    //    if (!_bIsWandering)
    //    {
    //        NavMeshPath path = new NavMeshPath();
    //        bool bPathValid = false;
    //        while (!bPathValid)
    //        {
    //            Vector3 wanderOffset = Random.insideUnitSphere * wanderRange;
    //            wanderOffset.y = 0;
    //            wanderPosition = transform.position + wanderOffset;
    //            bPathValid = agent.CalculatePath(wanderPosition, path);
    //        }
    //        _bIsWandering = true;
    //        agent.SetDestination(wanderPosition);
    //    }

    //    Debug.Log(agent.remainingDistance +" "+ agent.hasPath);

    //    _bIsWandering = agent.hasPath && agent.remainingDistance > 0.0f;

    //    // if the enemy reached the destination go back to attacking
    //    if (!_bIsWandering)
    //    {
    //        StartCoroutine(WanderToAttackState());
    //    }
    //}

    //IEnumerator WanderToAttackState()
    //{
    //    yield return new WaitForSeconds(attackDuration);
    //    _bIsAttacking = false;
    //    _currentState = ShooterState.Attack;
    //    yield return null;
    //}
}
