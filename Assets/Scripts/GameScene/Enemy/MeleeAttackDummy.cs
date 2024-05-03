using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test class used in the test scene, not an enemy entity
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

    // spawn a player-damaging collider every set time
    IEnumerator SpawnCollider()
    {
        yield return new WaitForSeconds(spawnInterval);

        Transform playerDamageCollider = Instantiate(colliderToSpawn, spawnPosition);
        Destroy(playerDamageCollider.gameObject, 0.1f);

        yield return null;

        StartCoroutine("SpawnCollider");
    }
}
