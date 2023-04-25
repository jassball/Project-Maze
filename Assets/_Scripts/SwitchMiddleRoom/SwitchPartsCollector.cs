using UnityEngine;
using TMPro;

public class SwitchPartsCollector : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public TextMeshProUGUI collectedPartsText;
    public GameObject[] switchParts; // Add this field to store the switch parts
    private int totalSwitchParts = 3;
    private int collectedSwitchParts = 0;
    private bool gameStarted = false; // Add this field to track if the game has started

    private void Start()
    {
        UpdateCollectedPartsText();
        collectedPartsText.gameObject.SetActive(false);
        foreach (GameObject part in switchParts) // Hide the switch parts at the start of the game
        {
            part.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0 && !gameStarted)
        {
            collectedPartsText.gameObject.SetActive(true);
            gameStarted = true;
            foreach (GameObject part in switchParts) // Activate the switch parts when the game starts
            {
                part.SetActive(true);
            }
        }
    }

    public void CollectSwitchPart()
    {
        collectedSwitchParts++;
        UpdateCollectedPartsText();
    }

    private void UpdateCollectedPartsText()
    {
        collectedPartsText.text = $"You have collected {collectedSwitchParts}/{totalSwitchParts} switch-parts";
    }
}