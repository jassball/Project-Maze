using UnityEngine;
using UnityEngine.UI;

public class ControlVoume  : MonoBehaviour
{
    public AudioSource bgm1; 
    public AudioSource soundEffect;
    public AudioSource soundEffect2;
    public AudioSource soundEffect3;
    public AudioSource soundEffect4;
    public AudioSource soundEffect5;
    public AudioSource soundEffect6;
    public AudioSource soundEffect7;
    public AudioSource soundEffect8;
    public AudioSource soundEffect9;
    public AudioSource soundEffect10;
    public AudioSource soundEffect11;
    public AudioSource soundEffect12;
    public AudioSource soundEffect13;
    public AudioSource soundEffect14;
    public AudioSource soundEffect15;
    public AudioSource soundEffect16;
    

    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(delegate { OnBgmSliderValueChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSfxSliderValueChanged(); });
    }

    private void OnBgmSliderValueChanged()
    {
        bgm1.volume = bgmSlider.value;
       
    }

    private void OnSfxSliderValueChanged()
    {
        soundEffect.volume = sfxSlider.value;
        soundEffect2.volume = sfxSlider.value;
        soundEffect3.volume = sfxSlider.value;
        soundEffect4.volume = sfxSlider.value;
        soundEffect5.volume = sfxSlider.value;
        soundEffect6.volume = sfxSlider.value;
        soundEffect7.volume = sfxSlider.value;
        soundEffect8.volume = sfxSlider.value;
        soundEffect9.volume = sfxSlider.value;
        soundEffect10.volume = sfxSlider.value;
        soundEffect11.volume = sfxSlider.value;
    }
}
