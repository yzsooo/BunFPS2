using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    public float damageAmount = 10.0f;
    public bool bDestroyOnContact = false;

    // Deal damage to the collider (probably a player)
    private void OnTriggerEnter(Collider other)
    {
        DamagePlayer(other.GetComponent<PlayerManager>());
    }

    // deal damage to the player's hp by set amount
    void DamagePlayer(PlayerManager pm)
    {
        // nullcheck
        if (pm == null) { return; }

        pm.playerEntity.HP.TakeDamage(damageAmount);
        if (bDestroyOnContact) { Destroy(gameObject); }

    }
}
