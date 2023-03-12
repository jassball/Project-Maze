using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{   
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject deathScreen;

    private void Start() {
        currentHealth = maxHealth;
        deathScreen.SetActive(false);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
