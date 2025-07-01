using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Til genstart af spil

public class KrabbeController : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;      // UI tekst til score
    public GameObject gameOverUI;           // UI til game over (skal sættes i Inspector)
    public GameObject explosionPrefab;      // Eksplosions-animation prefab (sæt i Inspector)

    private bool isGameOver = false;

    private void Start()
    {
        UpdateScoreText();
        gameOverUI.SetActive(false);        // Skjul game over tekst i starten
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

        // Vis Game Over UI
        gameOverUI.SetActive(true);

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

    // Tilføj evt. en funktion til at genstarte spillet (bruges til restart-knap)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
