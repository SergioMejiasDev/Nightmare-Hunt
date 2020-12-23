using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that manages the enemy generator.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies = null;
    public float spawnTime = 2.0f;
    [SerializeField] Transform[] spawnPoints = null;
    [SerializeField] PlayerHealth playerHealth = null;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerReset += StartSpawn;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerReset -= StartSpawn;
    }

    /// <summary>
    /// Function called through the delegate to start spawning enemies.
    /// </summary>
    void StartSpawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Function called every time an enemy spawns.
    /// </summary>
    void Spawn()
    {
        float randomNumber = Random.value;

        GameObject enemy;

        if (randomNumber <= 0.5)
        {
            enemy = enemies[0];
        }
        else if (randomNumber <= 0.8)
        {
            enemy = enemies[1];
        }
        else
        {
            enemy = enemies[2];
        }

        Transform enemyPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, enemyPosition.position, enemyPosition.rotation);
    }

    /// <summary>
    /// Coroutine that calls the spawn function after a few seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            
            if (playerHealth.currentHealth <= 0)
            {
                yield break;
            }
            
            Spawn();
        }
    }
}
