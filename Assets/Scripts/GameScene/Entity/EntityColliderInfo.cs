using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityColliderInfo : MonoBehaviour
{
    public Entity parentEntity;
    Collider thisCollider;

    // set collider of this colliderInfo
    private void Awake()
    {
        thisCollider = transform.GetComponent<Collider>();
    }
}
