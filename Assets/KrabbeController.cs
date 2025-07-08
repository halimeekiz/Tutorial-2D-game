using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Til genstart af spil

public class KrabbeController : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI highScoreText;  // UI tekst for high score
    public TextMeshProUGUI scoreText;      // UI tekst til score
    public GameObject gameOverUI;           // UI til game over (skal sættes i Inspector)
    public GameObject explosionPrefab;      // Eksplosions-animation prefab (sæt i Inspector)
    public GameObject restartButton;        // Restart knap (sæt i Inspector)
    private bool isGameOver = false;
    public float maxPlayTime = 120f;  // 3 minutter = 180 sekunder
    private float timeRemaining;
    public TextMeshProUGUI timerText;  // Reference til UI-tekst


    private void Start()
    {
    // Indlæs tidligere high score fra PlayerPrefs (gemt på computeren)
    highScore = PlayerPrefs.GetInt("HighScore", 0);

    // Start med fuld spilletid (3 minutter)
    timeRemaining = maxPlayTime;

    UpdateScoreText();
    UpdateHighScoreText();
    UpdateTimerText(); // ← viser "03:00" i starten

    gameOverUI.SetActive(false);    // Skjul game over tekst i starten
    if (restartButton != null)
        restartButton.SetActive(false);  // Skjul restart knap i starten
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;             // Hvis game er slut, ignorer collision

        if (other.CompareTag("Collectible"))
        {
            score++;
            UpdateScoreText();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bomb"))
        {
            Explode();
        }
        else
        {
            // Ignorer alt andet
        }
    }

    private void Explode()
    {
        Debug.Log("Boom! Krabben er død.");
        isGameOver = true;

        // Opdater high score hvis nødvendig
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Vis Game Over UI
        gameOverUI.SetActive(true);

        // Vis restart knap
        if (restartButton != null)
            restartButton.SetActive(true);

        UpdateHighScoreText();

        // Instantiér eksplosions animation på krabbens position
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);  // Sluk krabben, men slet den ikke
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    
    private void Update()
    {
        if (isGameOver) return;

        // Tæl tiden ned
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateTimerText();
            Explode();  // Spillet slutter, når tiden løber ud
            return;
        }

        UpdateTimerText();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

private void UpdateTimerText()
{
    if (timerText != null)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


    // Funktion til at genstarte spillet (kan kaldes fra restart knap)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    
}
