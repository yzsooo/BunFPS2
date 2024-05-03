using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HP class for enemy entities
public class EnemyHP : EntityHP
{

    // base TakeDamage func and call to detect the player that attacked it
    public override void TakeDamage(float damageAmount, Transform source = null)
    {
        base.TakeDamage(damageAmount, source);
        // force enemy to fight if taken damage
        EnemyBehaviour enm = GetComponent<EnemyBehaviour>();
        if (enm != null)
        {
            enm.DetectionTriggered(source.GetComponent<WeaponObject>().playerAttack.pm);
        }
    }
}
