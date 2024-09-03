using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float checkInterval = 0.25f; // Interval untuk memeriksa jarak
    [SerializeField] private float activationDistance = 7f;

    private Transform playerTransform;
    private bool canSpawn = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckDistanceToPlayer());
    }

    private IEnumerator CheckDistanceToPlayer()
    {
        WaitForSeconds wait = new(checkInterval);

        while (playerTransform != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < activationDistance)
            {
                if (!canSpawn)
                {
                    canSpawn = true;
                    StartCoroutine(Spawner());
                }
            }
            else
            {
                if (canSpawn)
                {
                    canSpawn = false;
                    StopCoroutine(Spawner());
                }
            }

            yield return wait;
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new(spawnRate);

        while (canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn, transform.position, transform.rotation);
        }
    }
}