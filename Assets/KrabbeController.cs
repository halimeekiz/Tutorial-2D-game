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

    private void Start()
    {
        // Indlæs tidligere high score fra PlayerPrefs (gemt på computeren)
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreText();
        UpdateHighScoreText();

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

        // Slet krabben (eller deaktiver den)
        Destroy(gameObject);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
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
