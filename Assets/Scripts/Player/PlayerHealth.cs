using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that manages the health of the player.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    public int currentHealth;

    [SerializeField] Slider sliderHealth = null;
    [SerializeField] Image heart = null;
    [SerializeField] Image imageDamage = null;
    [SerializeField] AudioClip deathClip = null;

    Animator anim;
    AudioSource audioSource;
    [SerializeField] AudioClip hurtClip = null;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    bool isHurt;

    [SerializeField] GameObject panelGameOver = null;
    [SerializeField] Text scoreText = null;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDeath;
    public static event PlayerDelegate OnPlayerReset;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (isHurt)
        {
            imageDamage.color = new Color(1.0f, 0.0f, 0.0f, 0.2f);
        }
        else
        {
            imageDamage.color = Color.Lerp(imageDamage.color, Color.clear, 10.0f * Time.deltaTime);
        }
        
        isHurt = false;
    }

    /// <summary>
    /// Function called every time the player takes damage.
    /// </summary>
    /// <param name="loseHealth">How much damage the player takes.</param>
    public void TakeDamage(int loseHealth)
    {
        isHurt = true;
        currentHealth -= loseHealth;
        sliderHealth.value = currentHealth;

        audioSource.clip = hurtClip;
        audioSource.Play();

        if (currentHealth <= 20)
        {
            heart.sprite = Resources.Load<Sprite>("HeartBlack");

            if (currentHealth <= 0)
            {
                Death();

                if (OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
            }
        }
    }

    /// <summary>
    /// Function called when the player dies.
    /// </summary>
    public void Death()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsDie", true);

        audioSource.clip = deathClip;
        audioSource.Play();

        StartCoroutine(GameOver());
    }

    /// <summary>
    /// Function called to reset the player to his position and initial state after the Game Over.
    /// </summary>
    public void ResetPlayer()
    {
        panelGameOver.SetActive(false);
        transform.position = Vector3.zero;
        anim.SetBool("IsDie", false);
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        currentHealth = startingHealth;
        sliderHealth.maxValue = startingHealth;
        sliderHealth.value = currentHealth;
        heart.sprite = Resources.Load<Sprite>("Heart");

        if (OnPlayerReset != null)
        {
            OnPlayerReset();
        }
    }

    /// <summary>
    /// Coroutine started after the player dies.
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        panelGameOver.SetActive(true);
        GameManager.gameManager.SaveHighScore();
        scoreText.enabled = false;
    }
}
