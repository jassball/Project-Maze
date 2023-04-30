using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(onValueChanged);
        
    }
    private void onValueChanged(float volume)
    {
        audioSource.volume = volume;
    }

   
}
