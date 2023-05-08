using TMPro;
using UnityEngine;
using System.Collections;

public class PickupSwitchPart : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI enemyPromt;
    public LayerMask whatIsPlayer;
    private bool playerInRange = false;
    public SwitchPartsCollector switchPartsCollector;
    public GameObject monsterSpawn;
    public GameObject pickUpPartSound;

    private AudioSource audioSource;

    void Start()
    {
        monsterSpawn.SetActive(false);
        audioSource = pickUpPartSound.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            playerInRange = true;
            pickupText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            switchPartsCollector.CollectSwitchPart();
            pickupText.gameObject.SetActive(false);
            monsterSpawn.SetActive(true);
            enemyPromt.gameObject.SetActive(true);
            audioSource.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            playerInRange = false;
            pickupText.gameObject.SetActive(false);
        }
    }
}