using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;
    public GameObject player;
    public float fadeDuration = 2f; 
    public Image blackStarterScreen;
    public AudioSource audioSource; // new public variable for the AudioSource component
    
    private bool playerInRange = false;
    private bool interactionDisabled = false;
    
    
    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = true;
            interactText.text = "Press E to open door";
            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!interactionDisabled && IsPlayerLayer(other.gameObject))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey) && !interactionDisabled)
        {
            TryOpenDoor();
        }

    }

    public void TryOpenDoor()
    {
        interactText.gameObject.SetActive(false);
        interactionDisabled = true;
        Debug.Log("player has won the game!!!Horry shit");
        
        // Play the audio clip
        audioSource.Play();
        
        StartCoroutine(FadeOutCoroutine());
    }

    bool IsPlayerLayer(GameObject obj)
    {
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }

    IEnumerator FadeOutCoroutine()
    {
        float startTime = Time.time;
        float alpha = 0f;
        blackStarterScreen.gameObject.SetActive(true); 

        // Loop until the alpha value reaches 1
        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeDuration);
            blackStarterScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        
        yield return new WaitForSeconds(1f); // Wait for 2 seconds after fade
        
        SceneManager.LoadScene("mainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}