using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Script that manages the health and score of the enemies.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    bool isDead;
    public bool capsuleCollider;
    [SerializeField] int score = 10;
    [SerializeField] AudioClip deathClip = null;
    AudioSource audioSource;
    Animator anim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        isDead = false;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Function called every time the enemy takes damage.
    /// </summary>
    /// <param name="damage">How much damage the enemy takes.</param>
    public void TakeDamage (int damage)
    {
        if (!isDead)
        {
            audioSource.Play();
            currentHealth = currentHealth - damage;

            if (currentHealth <= 0)
            {
                audioSource.clip = deathClip;
                audioSource.Play();

                Death();
            }
        }
    }

    /// <summary>
    /// Function called when the enemy dies.
    /// </summary>
    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");

        if (capsuleCollider)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            GetComponent<SphereCollider>().enabled = false;
        }

        GetComponent<EnemyController>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GameManager.gameManager.UpdateScore(score);
        StartCoroutine(DestroyEnemy());
    }

    /// <summary>
    /// Coroutine that removes the enemy from the screen a few seconds after dying.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
