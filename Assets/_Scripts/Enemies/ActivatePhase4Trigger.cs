using UnityEngine;

public class ActivatePhase4Trigger : MonoBehaviour
{
    public GameObject objectToActivate; // Assign the object in the Inspector
    public LayerMask WhatIsPlayer; // Configure the LayerMask in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player using the LayerMask
        if (((1 << other.gameObject.layer) & WhatIsPlayer) != 0)
        {
            objectToActivate.SetActive(true);
        }
    }
}
