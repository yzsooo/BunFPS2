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

    // deal damage to HP, update HUD and play visual effects of taking damage
    public override void TakeDamage(float damageAmount, Transform source = null)
    {
        base.TakeDamage(damageAmount, source);
        pm.HUD.HUDHealth.SetHealth(currentHP);
        takeDamageVFX.TakeDamageVisualEffects();
    }

    // add HP and update hud
    public void HealDamage(float healAmount)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP);
        pm.HUD.HUDHealth.SetHealth(currentHP);
    }
}
