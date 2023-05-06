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
    public PlayerHealth playerHealth;
   

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private GameObject playerCamHolder; // Reference to the player camera GameObject

    void Start()
    {
        resumButton.onClick.AddListener(Play);
        retry.onClick.AddListener(RestartGame);
        mainMenu.onClick.AddListener(MainMenuButton);
        Time.timeScale = 1f;

        playerCamHolder = GameObject.Find("playerCamHolder"); // Find the player camera GameObject by name
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
        if (playerHealth.currentHealth <=0)
        {
            PauseGamePanel.SetActive(false);
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

        // Freeze the position and rotation of the player camera
        if (playerCamHolder != null)
        {
            originalCameraPosition = playerCamHolder.transform.position;
            originalCameraRotation = playerCamHolder.transform.rotation;
        }
        //mute
        AudioListener.pause = true;
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

        // Restore the position and rotation of the player camera
        if (playerCamHolder != null)
        {
            playerCamHolder.transform.position = originalCameraPosition;
            playerCamHolder.transform.rotation = originalCameraRotation;
        }

        //unmute
        AudioListener.pause = false;

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
