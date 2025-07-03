using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnEntry
    {
        public GameObject enemyPrefab;
        public float startTime;      // When this enemy starts appearing
        public float baseWeight = 1; // Default spawn weight
        public float falloffRate = 0; // Per second (optional)
        public float growthRate = 0;  // Per second (optional)
    }


    [Header("Spawning")]
    public Transform player;
    public float spawnRadius = 10f;
    public float baseSpawnInterval = 3f;
    public float spawnRateDecreasePerLevel = 0.2f;
    public float minSpawnInterval = 0.5f;

    [Header("Enemy Pool")]
    public EnemySpawnEntry[] enemyPool;

    [Header("Wave Settings")]
    public float waveInterval = 45f;
    public float waveDuration = 10f;
    public float waveSpawnMultiplier = 0.5f;

    private float currentSpawnInterval;
    private bool isWave = false;

    void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        StartCoroutine(SpawnLoop());
        StartCoroutine(DifficultyScaler());
        StartCoroutine(EnemyWaveLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();

            float delay = isWave ? currentSpawnInterval * waveSpawnMultiplier : currentSpawnInterval;
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnEnemy()
    {
        if (player == null || enemyPool.Length == 0) return;

        float t = Time.timeSinceLevelLoad;
        List<GameObject> weightedChoices = new List<GameObject>();

        foreach (var entry in enemyPool)
        {
            if (t >= entry.startTime)
            {
                float timeSinceStart = t - entry.startTime;
                float weight = entry.baseWeight;

                // If falloffRate is set, reduce weight over time
                if (entry.falloffRate > 0)
                {
                    weight = Mathf.Max(0f, weight - timeSinceStart * entry.falloffRate);
                }

                // If growthRate is set, increase weight over time
                if (entry.growthRate > 0)
                {
                    weight += timeSinceStart * entry.growthRate;
                }

                int copies = Mathf.RoundToInt(weight);

                for (int i = 0; i < copies; i++)
                {
                    weightedChoices.Add(entry.enemyPrefab);
                }
            }
        }

        if (weightedChoices.Count == 0) return;

        Vector2 spawnDir = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + spawnDir * spawnRadius;

        GameObject selected = weightedChoices[Random.Range(0, weightedChoices.Count)];
        Instantiate(selected, spawnPos, Quaternion.identity);
    }


    IEnumerator DifficultyScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnRateDecreasePerLevel);
        }
    }

    IEnumerator EnemyWaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);
            isWave = true;
            Debug.Log("Wave started!");
            yield return new WaitForSeconds(waveDuration);
            isWave = false;
            Debug.Log("Wave ended.");
        }
    }
}
