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
        HP = GetComponent<EntityHP>();
        HP.ParentEntity = this;
        foreach (Transform t in Mesh)
        {
            t.GetComponent<EntityColliderInfo>().parentEntity = this;
        }
    }

    public virtual void OutOfHealth()
    {
        Debug.Log(EntityName + " is Dead");
    }
}
