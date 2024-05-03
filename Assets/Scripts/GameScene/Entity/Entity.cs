using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base entity class
public class Entity : MonoBehaviour
{
    public string EntityName;
    public EntityHP HP;
    public Transform Mesh;

    public virtual void Awake()
    {
        // Set HP var and HP's parententity
        HP = GetComponent<EntityHP>();
        HP.ParentEntity = this;
        // set each EntityColliderInfo's parentEntity
        foreach (Transform t in Mesh)
        {
            EntityColliderInfo colliderInfo = t.GetComponent<EntityColliderInfo>();
            if (colliderInfo != null) { colliderInfo.parentEntity = this; }
        }
    }

    // Destroy the gameObject when out of health
    public virtual void OutOfHealth()
    {
        Debug.Log(EntityName + " is Dead");
        Destroy(gameObject);
    }
}
