using UnityEngine;

public class DeactivateCrawlers : MonoBehaviour
{
    public GameObject[] enemies; // Assign the enemy GameObjects in the Inspector
    public LayerMask WhatIsPlayer; // Configure the LayerMask in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player using the LayerMask
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }
        }
    }
}
