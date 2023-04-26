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
    // Destroy this key object.
    Destroy(gameObject);
    }

    bool IsPlayerLayer(GameObject obj)
    {
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }
}