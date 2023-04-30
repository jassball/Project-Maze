using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu panel
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    private void Update()
    {
        // Check if the player has pressed the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the pause menu on/off
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        // Invert the isPaused flag
        isPaused = !isPaused;

        // Enable/disable the pause menu panel
        pauseMenuPanel.SetActive(isPaused);

        // Freeze/unfreeze the game time
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // The method that will be called when the Resume button is clicked
    public void Resume()
    {
        // Toggle the pause menu off
        TogglePauseMenu();
    }
}
