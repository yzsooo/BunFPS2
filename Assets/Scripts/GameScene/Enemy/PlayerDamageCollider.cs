using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    public float damageAmount = 10.0f;
    public bool bDestroyOnContact = false;

    private void OnTriggerEnter(Collider other)
    {
        // get the player object
        // deal damage to its health
        PlayerManager pm = other.GetComponent<PlayerManager>();
        if (pm != null)
        {
            pm.playerEntity.HP.TakeDamage(damageAmount);
            if (bDestroyOnContact) { Destroy(gameObject); }
        }
    }
}
