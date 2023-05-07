using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    public GameObject soundPlayer;
    public GameObject monster;
    public GameObject phase4Monster;
    public AudioClip mainAmbience;
    public AudioClip chaseMusic;
    public AudioClip phase4Music;
    // Start is called before the first frame update
    void Start()
    {
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();
    audioSource.clip = mainAmbience;
    audioSource.Play();
    audioSource.volume = 0.15f;
    }

    // Update is called once per frame
    void Update()
{
    AudioSource audioSource = soundPlayer.GetComponent<AudioSource>();

    if (monster.activeSelf && audioSource.clip != chaseMusic)
    {
        audioSource.clip = chaseMusic;
        
        audioSource.Play();
    }
    else if (!monster.activeSelf  && audioSource.clip != mainAmbience && !phase4Monster.activeSelf)
    {
        audioSource.clip = mainAmbience;
    
        audioSource.Play();
    }
    else if(phase4Monster.activeSelf && audioSource.clip != phase4Music)
    {
        audioSource.clip = phase4Music;
        
        audioSource.Play();
    }
}
}
