using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;
    public int numberToSpawn = 10;
    public float spawnRadius = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;

            Vector3 spawnPosition = transform.position + randomOffset;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
