using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHP : EntityHP
{
    public PlayerManager pm;
    public float startHP;
    PlayerTakeDamageVisualEffects takeDamageVFX;
    private void Awake()
    {
        currentHP = startHP;
        pm.HUD.HUDHealth.SetHealth(currentHP);
        takeDamageVFX = GetComponent<PlayerTakeDamageVisualEffects>();
    }

    public override void TakeDamage(float damageAmount, Transform source = null)
    {
        base.TakeDamage(damageAmount, source);
        pm.HUD.HUDHealth.SetHealth(currentHP);
        takeDamageVFX.TakeDamageVisualEffects();
    }

    public void HealDamage(float healAmount)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP);
        pm.HUD.HUDHealth.SetHealth(currentHP);
    }
}
