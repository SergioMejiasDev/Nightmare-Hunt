using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that controls the main functions of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] DifficultyManager difficultyManager = null;

    int score;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text scoreMenuText = null;
    [SerializeField] Text highScoreText = null;

    [SerializeField] GameObject panelHealth = null;
    [SerializeField] GameObject panelLanguage = null;
    [SerializeField] GameObject panelGameOver = null;
    [SerializeField] GameObject panelPause = null;
    [SerializeField] GameObject panelMenu = null;
    [SerializeField] GameObject panelOptions = null;
    [SerializeField] GameObject panelLoading = null;
    [SerializeField] GameObject panelHelp = null;

    [SerializeField] GameObject panelFade = null;
    Image fadeImage;

    private void Awake()
    {
        gameManager = this;
        fadeImage = panelFade.GetComponent<Image>();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += DeathPlayer;
        PlayerHealth.OnPlayerReset += ResetPlayer;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= DeathPlayer;
        PlayerHealth.OnPlayerReset -= ResetPlayer;
    }

    void Start()
    {
        StartCoroutine(InitialFade());
    }

    /// <summary>
    /// Function that is called every time the score increases.
    /// </summary>
    /// <param name="scoreIncrease">How much the score increases.</param>
    public void UpdateScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = "SCORE: " + score.ToString();
    }

    /// <summary>
    /// Function that completely closes the game.
    /// </summary>
    public void CloseGame()
    {
        StartCoroutine(ExitGame());
    }

    /// <summary>
    /// Function that starts the game from the main menu.
    /// </summary>
    public void LoadGame()
    {
        StartCoroutine(LoadScene(1));
    }

    /// <summary>
    /// Function that ends the game and returns to the main menu.
    /// </summary>
    public void BackToMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    /// <summary>
    /// Function called during Game Over to save the highest scores differentiating the level of difficulty.
    /// </summary>
    public void SaveHighScore()
    {
        scoreMenuText.text = "SCORE: " + score.ToString();

        int gameType = difficultyManager.difficulty;

        int highScoreLoaded;

        if (gameType == 1)
        {
            if (PlayerPrefs.HasKey("HighScore1"))
            {
                highScoreLoaded = PlayerPrefs.GetInt("HighScore1");
            }

            else
            {
                highScoreLoaded = 0;
            }

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore1", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }

        else if (gameType == 2)
        {
            if (PlayerPrefs.HasKey("HighScore2"))
            {
                highScoreLoaded = PlayerPrefs.GetInt("HighScore2");
            }

            else
            {
                highScoreLoaded = 0;
            }

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore2", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }

        else if (gameType == 3)
        {
            if (PlayerPrefs.HasKey("HighScore3"))
            {
                highScoreLoaded = PlayerPrefs.GetInt("HighScore3");
            }

            else
            {
                highScoreLoaded = 0;
            }

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore3", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }
    }

    /// <summary>
    /// Function called through delegate that resets the necessary parameters to continue playing after Game Over.
    /// </summary>
    void ResetPlayer()
    {
        panelLanguage.SetActive(false);
        panelHealth.SetActive(true);
        scoreText.enabled = true;
        score = 0;
        scoreText.text = "SCORE: 0";
    }

    /// <summary>
    /// Function called through delegate when player dies.
    /// </summary>
    void DeathPlayer()
    {
        panelHealth.SetActive(false);
        scoreText.enabled = false;
    }

    /// <summary>
    /// Function that opens the panel to choose the difficulty.
    /// </summary>
    public void OpenPanelDifficulty()
    {
        panelGameOver.SetActive(false);
        panelHelp.SetActive(false);
        panelLanguage.SetActive(true);
    }

    /// <summary>
    /// Function that opens and closes the pause panel.
    /// </summary>
    public void PauseGame()
    {
        if (panelPause.activeSelf == false)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Function that opens and closes the options panel.
    /// </summary>
    public void MenuOptions()
    {
        if (panelOptions.activeSelf == false)
        {
            panelOptions.SetActive(true);
            panelMenu.SetActive(false);
        }
        else
        {
            panelOptions.SetActive(false);
            panelMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Coroutine started every time we change scene.
    /// </summary>
    /// <param name="sceneNumber">Scene we want to load.</param>
    /// <returns></returns>
    IEnumerator LoadScene(int sceneNumber)
    {
        Time.timeScale = 1;
        panelFade.SetActive(true);
        
        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a < 1)
        {
            alphaValue = imageColor.a + (2 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        panelLoading.SetActive(true);
        SceneManager.LoadScene(sceneNumber);
    }

    /// <summary>
    /// Corroutine that manages the initial fade.
    /// </summary>
    /// <returns></returns>
    public IEnumerator InitialFade()
    {
        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a > 0)
        {
            alphaValue = imageColor.a - (1 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        panelFade.SetActive(false);
    }

    /// <summary>
    /// Corroutine that starts the fade and closes the game.
    /// </summary>
    /// <returns></returns>
    IEnumerator ExitGame()
    {
        panelFade.SetActive(true);

        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a < 1)
        {
            alphaValue = imageColor.a + (2 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        yield return new WaitForSeconds(1);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
