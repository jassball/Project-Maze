using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public string interactKey = "e";
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI interactText;
    public GameObject player;
    public KeyPickup keyPickupScript;
    public float rotationAngle = 90f;
    public float rotationSpeed = 2f;

    private bool playerInRange = false;
    private bool doorOpened = false;
    private bool interactionDisabled = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        interactText.gameObject.SetActive(false);
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
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

        if (doorOpened)
        {
            OpenDoor();
        }
    }

    public void TryOpenDoor()
    {
        if (keyPickupScript.keyUIImage.gameObject.activeInHierarchy)
        {
            // Set the keyUIImage to false to show the key is used.
            keyPickupScript.keyUIImage.gameObject.SetActive(false);

            // Open the door
            doorOpened = true;

            // Deactivate the interactText
            interactText.gameObject.SetActive(false);

            // Disable further interactions
            interactionDisabled = true;
        }
        else
        {
            // If the player doesn't have a key, update the interactText.
            interactText.text = "You don't have the key";
        }
    }

    void OpenDoor()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
    }

    bool IsPlayerLayer(GameObject obj)
    {
        return whatIsPlayer == (whatIsPlayer | (1 << obj.layer));
    }
}
