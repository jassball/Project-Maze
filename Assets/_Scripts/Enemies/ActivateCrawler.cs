using UnityEngine;

public class ActivateCrawler : MonoBehaviour
{
    public GameObject enemy; // Assign the enemy GameObject in the Inspector
    public LayerMask WhatIsPlayer; // Configure the LayerMask in the Inspector
    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player using the LayerMask
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            enemy.SetActive(true);
            myCollider.enabled = false; // Disable the collider
        }
    }
}
