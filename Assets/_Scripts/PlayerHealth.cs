using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image screenOverlay; // reference to the full-screen Image component
    public GameObject gameOverScreen;
    public GameObject gameOverText;
    public GameObject restart;
    public Button restartButton;

    private bool isGameOver = false;

    private void Start()
    {
        currentHealth = maxHealth;
        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);
        restart.SetActive(false);
        restartButton.onClick.AddListener(Restart);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f; // Pause the game
            gameOverScreen.SetActive(true);
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        Color startColor = screenOverlay.color;
        Color endColor = Color.black;
        float duration = 1f; // adjust as needed

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use unscaledDeltaTime to keep time consistent
            screenOverlay.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            yield return null;
        }

        gameOverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restart.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Resume the game
    }
}