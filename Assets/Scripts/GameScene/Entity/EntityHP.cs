using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

// base HP class
public class EntityHP : MonoBehaviour
{
    [SerializeField]
    protected float currentHP;
    public float maxHP;
    public bool bHealthIsEmpty = false;
    Entity parentEntity;
    // set parent entity class
    public Entity ParentEntity
    {
        set { parentEntity = value; }
    }

    private void Awake()
    {
        currentHP = maxHP;
    }

    // Reduce hp by the damage amount
    // if it runs out of health then call the parentEntity's function for dealing with running out of health
    public virtual void TakeDamage(float damageAmount, Transform source = null)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0 && !bHealthIsEmpty)
        {
            bHealthIsEmpty = true;
            parentEntity.OutOfHealth();
        }
    }
}
