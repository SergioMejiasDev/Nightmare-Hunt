using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class in charge of the movement of the enemies.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public float attackRange = 25.0f;

    Transform player;
    NavMeshAgent nav;
    Animator anim;
    EnemyHealth enemyHealth;

    bool followPlayer;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerDeath;
        PlayerHealth.OnPlayerReset += PlayerReset;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDeath;
        PlayerHealth.OnPlayerReset -= PlayerReset;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        followPlayer = true;
    }

    void Update()
    {
        if ((Vector3.Distance(transform.position, player.position) < attackRange) && (enemyHealth.currentHealth > 0))
        {
            if (followPlayer)
            {
                nav.SetDestination(player.position);
            }
        }

        anim.SetBool("PlayerDead", !followPlayer);
    }

    /// <summary>
    /// Function called through delegate when player dies.
    /// </summary>
    void PlayerDeath()
    {
        followPlayer = false;
        nav.enabled = false;
    }

    /// <summary>
    /// Function called through the delegate when the player resets.
    /// </summary>
    void PlayerReset()
    {
        Destroy(gameObject);
    }
}
