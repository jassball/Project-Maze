using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    public GameObject soundPlayer;
    public GameObject monster;
    public AudioClip mainAmbience;
    public AudioClip chaseMusic;
    public float chaseVolume;
    // Start is called before the first frame update
    void Start()
    {
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
    audioSource.clip = mainAmbience;
    audioSource.Play();
    }

    // Update is called once per frame
    void Update()
{
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();

    if (monster.activeSelf && audioSource.clip != chaseMusic)
    {
        audioSource.clip = chaseMusic;
        audioSource.volume = chaseVolume;
        audioSource.Play();
    }
    else if (!monster.activeSelf  && audioSource.clip != mainAmbience)
    {
        audioSource.clip = mainAmbience;
        audioSource.volume = 0.15f;
        audioSource.Play();
    }
}
}
