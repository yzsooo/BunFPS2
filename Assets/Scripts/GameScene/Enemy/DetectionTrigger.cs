using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    [SerializeField]
    bool _bCanSeePlayer = false;
    public EnemyBehaviour parent;


    // Check if the collided object is a player, check if its in line of sight then trigger detection if it is
    private void OnTriggerStay(Collider other)
    {
        // ignore if collider is not player
        if (!other.CompareTag("Player")) { return; }
        // check if the player is in line of sight and call to trigger detection if it is
        _bCanSeePlayer = CheckPlayerInLineOfSight(other);
        if (_bCanSeePlayer) { parent.DetectionTriggered(other.GetComponent<PlayerManager>()); }
    }

    // returns true if the player is in line of sight
    bool CheckPlayerInLineOfSight(Collider player)
    {
        bool bIsLOS;

        // Shoot a raycast towards the player
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toPlayer = Vector3.Normalize(player.transform.position - transform.position);
        RaycastHit hit;
        int entityLayerMask = 1 << 8;
        entityLayerMask = ~entityLayerMask;
        bool ray = Physics.Raycast(transform.position, toPlayer, out hit, Mathf.Infinity, entityLayerMask);
        // raycast is valid if a player's colliderInfo is detected by the ray
        bool raycastValid = (ray && hit.collider.GetComponent<EntityColliderInfo>() != null);

        // Check if Line of sight detection is valid
        // detection is valid if;
        // the player is in front of this enemy and
        // raycast is valid
        bIsLOS = (Vector3.Dot(forward, toPlayer) > 0) && raycastValid;

        return bIsLOS;
    }
}
