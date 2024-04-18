using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHP : EntityHP
{
    PlayerManager pm;
    public float startHP;
    private void Awake()
    {
        currentHP = startHP;
        pm = GetComponent<PlayerManager>();
        pm.HUD.HUDHealth.SetHealth(currentHP);
    }

    public override void TakeDamage(float damageAmount, Transform source = null)
    {
        base.TakeDamage(damageAmount, source);
        pm.HUD.HUDHealth.SetHealth(currentHP);
    }

    public void HealDamage(float healAmount)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP);
        pm.HUD.HUDHealth.SetHealth(currentHP);
    }
}
