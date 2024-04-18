using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPlayerHP : DebugUISubmenuButton
{
    [SerializeField]
    float adjustAmount;
    [SerializeField]
    bool bHeal;

    public override void UseButton()
    {
        PlayerHP HPComponent = GameSceneManager.GameSceneManagerInstance.Player.playerEntity.HP as PlayerHP;
        if (bHeal)
        {
            HPComponent.HealDamage(adjustAmount);
        }
        else
        {
            HPComponent.TakeDamage(adjustAmount);
        }
        //if (HPAmountToAdjust > 0)
        //{
        //    // Heal
        //    HPComponent.HealDamage(HPAmountToAdjust);
        //}
        //else
        //{
        //    // deal damage
        //    HPComponent.TakeDamage(HPAmountToAdjust);
        //}
    }
}
