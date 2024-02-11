using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EntityHP : MonoBehaviour
{
    public float maxHP;
    float currentHP;

    public bool bHealthIsEmpty = false;

    public static event Action HealthIsEmpty;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damageAmount, Transform source = null)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0 && !bHealthIsEmpty)
        {
            currentHP = 0;
            bHealthIsEmpty = true;
            HealthIsEmpty?.Invoke();
        }
    }
}
