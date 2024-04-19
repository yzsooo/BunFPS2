using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    [SerializeField]
    bool _bCanSeePlayer = false;
    public DetectChaseDummy parent;

    private void OnTriggerStay(Collider other)
    {
        // ignore if collider is not player
        if (!other.CompareTag("Player")) { return; }

        // check if player is within LOS
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Vector3.Normalize(other.transform.position - transform.position);
        // check if player can be seen with raycast
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, toOther, out hit);
        bool raycastValid = (ray && hit.collider.GetComponent<EntityColliderInfo>() != null);
        // detection is valid if;
        // the player is in front of the enemy and
        // theres a valid direct ray to the player
        _bCanSeePlayer = (Vector3.Dot(forward, toOther) > 0) && raycastValid;

        if (_bCanSeePlayer) { parent.DetectionTriggered(other.GetComponent<PlayerManager>()); }
    }
}
