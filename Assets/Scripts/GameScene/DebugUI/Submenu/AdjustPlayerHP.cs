using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPlayerHP : DebugUISubmenuButton
{
    [SerializeField]
    float adjustAmount;
    [SerializeField]
    bool bHeal;

    // damage or heal the player's hp by adjustAmount
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
    }
}
