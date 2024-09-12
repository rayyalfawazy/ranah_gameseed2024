using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float checkInterval = 0.25f; // Interval untuk memeriksa jarak
    [SerializeField] private float activationDistance = 7f;

    private float distanceToPlayer;

    private Transform playerTransform;
    private bool canSpawn = false;
    private bool isSpawning = false; // Untuk mencegah multiple coroutine

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckDistanceToPlayer());
    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        Debug.DrawLine(transform.position, playerTransform.position,Color.red);
    }

    private IEnumerator CheckDistanceToPlayer()
    {
        WaitForSeconds wait = new(checkInterval);

        while (playerTransform != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < activationDistance && !isSpawning)
            {
                canSpawn = true;
                isSpawning = true;

                // Spawn langsung saat player mendekat
                //SpawnEnemy();

                // Lanjutkan dengan spawner coroutine
                StartCoroutine(Spawner());
            }
            else if (distanceToPlayer >= activationDistance && isSpawning)
            {
                canSpawn = false;
                isSpawning = false;
                StopCoroutine(Spawner());
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
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[rand];
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}
