using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityColliderInfo : MonoBehaviour
{
    public Entity parentEntity;
    Collider thisCollider;

    private void Awake()
    {
        thisCollider = transform.GetComponent<Collider>();
    }
}
