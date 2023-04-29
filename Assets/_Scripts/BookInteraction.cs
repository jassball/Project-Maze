using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookInteraction : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public Image bookImage;
    public LayerMask whatIsPlayer;
    public EntryMazeDoor door;
    public AudioSource bookAudioSource; // Add this line
    public AudioClip bookOpenSound; // Add this line
    public GameObject EntryDoorSound;
    public AudioClip OpenDoorSound; 

    private bool isPlayerInRange = false;
    private bool isBookOpen = false;
    private bool hasOpenedDoor = false;

    void Start()
    {
        promptText.gameObject.SetActive(false);
        bookImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = true;
            UpdatePromptText();
            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = false;
            isBookOpen = false;
            promptText.gameObject.SetActive(false);
            bookImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isBookOpen = !isBookOpen;
            bookImage.gameObject.SetActive(isBookOpen);
            UpdatePromptText();

            if (isBookOpen) // Add this block
            {
                bookAudioSource.clip = bookOpenSound;
                bookAudioSource.Play();
            }

            if(!hasOpenedDoor && isBookOpen) {
                door.Open();
                hasOpenedDoor = true;
                EntryDoorSound.GetComponent<AudioSource>().clip = OpenDoorSound;
                EntryDoorSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return ((1 << other.gameObject.layer) & whatIsPlayer) != 0;
    }

    private void UpdatePromptText()
    {
        promptText.text = isBookOpen ? "Press \"E\" to close book" : "Press \"E\" to open book";
    }
}
