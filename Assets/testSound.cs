using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSound : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SoundSlider;

    
    public AudioSource musicSource;
    public AudioSource soundSource;
    
   
    public AudioClip bookClosing;
    public AudioClip bookOpen;
    public AudioClip buildLever;
    public AudioClip ChaseMus;
    public AudioClip crawlingSound;
    public AudioClip DarkFog;
    public AudioClip doorSlam;
    public AudioClip HatchOpen;
    public AudioClip HittingGround;
    public AudioClip ItemPickup;
    public AudioClip jogging;
    public AudioClip lastChase;
    public AudioClip leverPull;
    public AudioClip lockpicking;
    public AudioClip menuMusic;
    public AudioClip metalDoor;
    public AudioClip metalicDoor;
    public AudioClip monsterEffect;
    public AudioClip monsterScreech;
    public AudioClip monsterScream;
    public AudioClip openSqueak;
    public AudioClip painful;
    public AudioClip running;
    public AudioClip slowDoor;
    public AudioClip sprint;
    public AudioClip switchBuild;
    public AudioClip torchBurning;
    public AudioClip torchGetsLit;


    // Start is called before the first frame update
    void Start()
    {
        MusicSlider.onValueChanged.AddListener(MusicVoulume);
        SoundSlider.onValueChanged.AddListener(SoundVolume);
        
    }

    void MusicVoulume(float volume)
    {
        /*
        DarkFog.volume = volume;
        ChaseMus.volume = volume;
        lastChase.volume = volume;*/
        musicSource.volume = volume;


    }

    void SoundVolume(float volume)
    {
        /*
        bookClosing.volume = volume;
        bookOpen.volume = volume;
        buildLever.volume = volume;
        doorSlam.volume = volume;
        HatchOpen.volume = volume;
        ItemPickup.volume =volume;
        leverPull.volume =volume;
        lockpicking.volume =volume;
        metalDoor.volume =volume;
        metalicDoor.volume =volume;
        monsterEffect.volume =volume;
        monsterScreech.volume =volume;
        monsterScream.volume =volume;
        openSqueak.volume =volume;
        painful.volume =volume;
        running.volume =volume;
        slowDoor.volume =volume;
        sprint.volume =volume;
        switchBuild.volume =volume;
        torchBurning.volume =volume;
        torchGetsLit.volume =volume;*/
        soundSource.volume = volume;
        
    }

   
}
