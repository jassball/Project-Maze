using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase4Monster : MonoBehaviour
{
    public GameObject monsterSpawn;
    public TextMeshProUGUI runPrompt;

    void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            runPrompt.gameObject.SetActive(true);
            monsterSpawn.SetActive(true);
            Debug.Log("triggered");
        }
    }
}
