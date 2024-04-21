using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Vector3 direction;

    float despawnTimer = 5.0f;

    private void Update()
    {
        if (direction != null)
        {
            transform.position += direction * Time.deltaTime;
        }
    }

    public void SpawnProjectile(float newSpeed, float newDamage, Vector3 newDirection)
    {
        GetComponent<PlayerDamageCollider>().damageAmount = newDamage;
        GetComponent<PlayerDamageCollider>().bDestroyOnContact = true;
        direction = newDirection * newSpeed;

        StartCoroutine("Despawn");
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
        yield return null;
    }
}
