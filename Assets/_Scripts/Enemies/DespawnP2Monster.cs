using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnP2Monster : MonoBehaviour
{
    public GameObject P2enemy;
    public LayerMask whatIsPlayer;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsPlayer) != 0)
        {
            P2enemy.SetActive(false);
            boxCollider.enabled = false;
        }
    }
}
