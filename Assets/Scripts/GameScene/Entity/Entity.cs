using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string EntityName;
    public EntityHP HP;
    public Transform Mesh;

    public virtual void Awake()
    {
        if (HP == null)
        {
            HP = GetComponent<EntityHP>();
        }
        HP.ParentEntity = this;
        foreach (Transform t in Mesh)
        {
            EntityColliderInfo colliderInfo = t.GetComponent<EntityColliderInfo>();
            if (colliderInfo != null) { colliderInfo.parentEntity = this; }
        }
    }

    public virtual void OutOfHealth()
    {
        Debug.Log(EntityName + " is Dead");
        Destroy(gameObject);
    }
}
