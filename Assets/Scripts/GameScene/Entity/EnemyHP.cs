using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : EntityHP
{

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
