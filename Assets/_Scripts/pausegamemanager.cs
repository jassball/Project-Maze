using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pausegamemanager : MonoBehaviour
{
    public Button resumButton;
    public Button mainMenu;
    public Button retry;
    public static bool Paused = false;
    public GameObject PauseGamePanel;
    private bool CursorWasLocked = false;

    void Start()
    {
        resumButton.onClick.AddListener(Play);
        retry.onClick.AddListener(RestartGame);
        mainMenu.onClick.AddListener(MainMenuButton);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseGamePanel.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        CursorWasLocked = Cursor.lockState == CursorLockMode.Locked;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        PauseGamePanel.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.visible = false;

        if (CursorWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Debug.Log("resumButton!!");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        Paused = false;
        Debug.Log("back to menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Game is Restarted!!");
    }
}
