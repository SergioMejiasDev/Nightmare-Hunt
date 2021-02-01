using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class that modifies the variables affected by the difficulty.
/// </summary>
public class DifficultyManager : MonoBehaviour
{
    [SerializeField] GameObject zombunny = null;
    [SerializeField] GameObject zombear = null;
    [SerializeField] GameObject hellephant = null;

    [SerializeField] EnemyManager enemyManager = null;

    public int difficulty;

    /// <summary>
    /// Function called every time the difficulty is changed.
    /// </summary>
    /// <param name="newDifficulty">Difficulty to be established, ordered from 1 to 3 from easiest to hardest.</param>
    public void ChangeDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
        
        if (difficulty == 1)
        {
            enemyManager.spawnTime = 1;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 20;
            zombear.GetComponent<EnemyHealth>().startingHealth = 50;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 100;

            zombunny.GetComponent<EnemyController>().attackRange = 25.0f;
            zombear.GetComponent<EnemyController>().attackRange = 25.0f;
            hellephant.GetComponent<EnemyController>().attackRange = 25.0f;

            zombunny.GetComponent<NavMeshAgent>().speed = 5;
            zombear.GetComponent<NavMeshAgent>().speed = 3.5f;
            hellephant.GetComponent<NavMeshAgent>().speed = 2;
        }

        else if (difficulty == 2)
        {
            enemyManager.spawnTime = 1f;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 40;
            zombear.GetComponent<EnemyHealth>().startingHealth = 75;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 150;

            zombunny.GetComponent<EnemyController>().attackRange = 100.0f;
            zombear.GetComponent<EnemyController>().attackRange = 100.0f;
            hellephant.GetComponent<EnemyController>().attackRange = 100.0f;

            zombunny.GetComponent<NavMeshAgent>().speed = 7.5f;
            zombear.GetComponent<NavMeshAgent>().speed = 5;
            hellephant.GetComponent<NavMeshAgent>().speed = 4;
        }

        else if (difficulty == 3)
        {
            enemyManager.spawnTime = 1.5f;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 50;
            zombear.GetComponent<EnemyHealth>().startingHealth = 100;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 200;

            zombunny.GetComponent<EnemyController>().attackRange = 1000.0f;
            zombear.GetComponent<EnemyController>().attackRange = 1000.0f;
            hellephant.GetComponent<EnemyController>().attackRange = 1000.0f;

            zombunny.GetComponent<NavMeshAgent>().speed = 10;
            zombear.GetComponent<NavMeshAgent>().speed = 7.5f;
            hellephant.GetComponent<NavMeshAgent>().speed = 6;
        }
    }
}
