using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EntityHP : MonoBehaviour
{
    [SerializeField]
    protected float currentHP;
    public float maxHP;
    public bool bHealthIsEmpty = false;
    Entity parentEntity;
    public Entity ParentEntity
    {
        set { parentEntity = value; }
    }

    public static event Action HealthIsEmpty;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damageAmount, Transform source = null)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0 && !bHealthIsEmpty)
        {
            currentHP = 0;
            bHealthIsEmpty = true;
            parentEntity.OutOfHealth();
        }
    }
}
