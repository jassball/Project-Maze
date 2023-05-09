using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GodModeSwitchParts : MonoBehaviour
{
    public GameObject player;  // Reference to your player
    public GameObject part1;  // Reference to the first part
    public GameObject part2;  // Reference to the second part
    public GameObject part3;  // Reference to the third part
    public TextMeshProUGUI switchPartCollector;

    private bool spawnMode = false;
    public float distance = 2f;  // The distance in front of the player where the parts will spawn
    public float spacing = 1f;  // The spacing between the parts
    public float downOffsetPart1 = 0.5f;  // The downward offset
    public float downOffsetPart2 = 0.5f;  // The downward offset
    public float downOffsetPart3 = 0.5f;  // The downward offset

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spawnMode = !spawnMode;  // Toggle spawn mode

            if (spawnMode)
            {
                SpawnParts();
            }
        }
    }

    void SpawnParts()
    {
        switchPartCollector.gameObject.SetActive(true);
        part1.SetActive(true);
        part2.SetActive(true);
        part3.SetActive(true);

        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward;

        // Calculate the positions for the parts
        Vector3 part1Pos = playerPos + playerForward * distance + player.transform.right * -spacing;
        Vector3 part2Pos = playerPos + playerForward * distance;
        Vector3 part3Pos = playerPos + playerForward * distance + player.transform.right * spacing;

        // Apply the downward offset
        part1Pos.y -= downOffsetPart1;
        part2Pos.y -= downOffsetPart2;
        part3Pos.y -= downOffsetPart3;

        // Set the positions of the parts
        part1.transform.position = part1Pos;
        part2.transform.position = part2Pos;
        part3.transform.position = part3Pos;
    }
}
