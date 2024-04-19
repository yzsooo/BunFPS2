using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackDummy : MonoBehaviour
{

    // spawning collider
    public float spawnInterval;
    public Transform colliderToSpawn;
    public Transform spawnPosition;

    private void Awake()
    {
        StartCoroutine("SpawnCollider");
    }

    IEnumerator SpawnCollider()
    {
        yield return new WaitForSeconds(spawnInterval);

        Transform playerDamageCollider = Instantiate(colliderToSpawn, spawnPosition);
        Destroy(playerDamageCollider.gameObject, 0.1f);

        yield return null;

        StartCoroutine("SpawnCollider");
    }
}
