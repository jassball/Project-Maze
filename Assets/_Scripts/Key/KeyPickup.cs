using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class KeyPickup : MonoBehaviour
{
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;

    private KeyOutline keyOutline;
    private bool playerInRange = false;
    public Image keyUIImage;

    public AudioClip pickupSound;
    public GameObject audioPlayerPrefab; // Add this line to declare an AudioPlayer prefab

    void Start()
    {
        keyOutline = GetComponent<KeyOutline>();
        interactText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerLayer(other.gameObject))
        {
            playerInRange = true;
            keyOutline.ShowOutline();
            interactText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayerLayer(other.gameObject))
        {
            playerInRange = false;
            keyOutline.HideOutline();
            interactText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            PickUpKey();
        }
    }

    void PickUpKey()
    {
        // Add the key to the player's inventory.
        // Deactivate the interactText
        interactText.gameObject.SetActive(false);
        // Show the key UIImage
        keyUIImage.gameObject.SetActive(true);
        // Play the pickup sound
        PlaySound(pickupSound);
        // Destroy this key object
        Destroy(gameObject);
    }

    void PlaySound(AudioClip clip)
    {
        // Instantiate the AudioPlayer prefab
        GameObject audioPlayer = Instantiate(audioPlayerPrefab);
        // Get the AudioSource component from the AudioPlayer
        AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
        // Assign the sound clip to the AudioSource
        audioSource.clip = clip;
        // Play the sound
        audioSource.Play();
        // Destroy the AudioPlayer object after the sound has finished playing
        Destroy(audioPlayer, clip.length);
    }

    bool IsPlayerLayer(GameObject obj)
    {
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }
}