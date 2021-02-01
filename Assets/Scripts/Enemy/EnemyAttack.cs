using UnityEngine;

/// <summary>
/// Class in charge of enemy attacks.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int attackDamage = 5;
    [SerializeField] float cadency = 1.0f;
    bool playerHurt;
    bool playerDeath;

    GameObject player;
    Animator anim;

    float timer;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDeath;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        playerDeath = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if ((playerHurt) && (timer >= cadency))
        {
            timer = 0;

            if (!playerDeath)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (player == other.gameObject)
        {
            playerHurt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player == other.gameObject)
        {
            playerHurt = false;
        }
    }

    /// <summary>
    /// Function called through delegate when player dies.
    /// </summary>
    void PlayerDeath()
    {
        playerDeath = true;
        anim.SetBool("PlayerDead", true);
    }
}
