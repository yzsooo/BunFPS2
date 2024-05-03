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
        // move towards the dirction if set
        if (direction != null)
        {
            transform.position += direction * Time.deltaTime;
        }
    }

    // called when initializing a new enemy projectile
    // move to a new direction at a set speed
    // set damage to the collider of this projectile
    // start coroutine to destory this object
    public void SpawnProjectile(float newSpeed, float newDamage, Vector3 newDirection)
    {
        GetComponent<PlayerDamageCollider>().damageAmount = newDamage;
        GetComponent<PlayerDamageCollider>().bDestroyOnContact = true;
        direction = newDirection * newSpeed;

        StartCoroutine("Despawn");
    }

    // destory this gameobject after a set time
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
        yield return null;
    }

    // If it collides with a non trigger object (like a wall) destroy this gameobjcet
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        Destroy(gameObject);
    }

    // check if the collider is a player and destroy this object
    // dealing damage is done in the PlayerDamageCollider
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() == null)
        {
            Destroy(gameObject);
        }
    }
}
