using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4Monster : MonoBehaviour
{
    public GameObject monsterSpawn;

    void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            monsterSpawn.SetActive(true);
            Debug.Log("triggered");
        }
    }
}
