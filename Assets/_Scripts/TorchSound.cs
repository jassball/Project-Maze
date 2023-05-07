using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSound : MonoBehaviour
{
    public AudioClip soundClip1;
    public AudioClip soundClip2;
    public float volume; 

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip1;
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundClip2;
            audioSource.loop = true;
            audioSource.volume = volume; // Set the volume of the second sound clip
            audioSource.Play();
        }
    }
}
