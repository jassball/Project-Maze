using UnityEngine;
using UnityEngine.UI;

public class ControlVoume  : MonoBehaviour
{
    public AudioSource music; 
    public AudioSource jailDoor;
    public AudioSource jailDoorRigth;
    public AudioSource knifeAudio;
    public AudioSource bookBigBrown;
    public AudioSource player;
    public AudioSource phase1Enemy;
    public AudioSource phase4Enemy;
    public AudioSource enteryDoor;
    public AudioSource torch;
    public AudioSource hatch;
    public AudioSource pickUp;
    public AudioSource door1;
    public AudioSource doorEndOfPh3;
    public AudioSource soundEffect14;
    public AudioSource soundEffect15;
    public AudioSource soundEffect16;
    

    public Slider musicSlider;
    public Slider audioSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { OnmusicSliderValueChanged(); });
        audioSlider.onValueChanged.AddListener(delegate { OnaudioSliderValueChanged(); });
    }

    private void OnmusicSliderValueChanged()
    {
        music.volume = musicSlider.value;
       
    }

    private void OnaudioSliderValueChanged()
    {
        music.volume = audioSlider.value;
        jailDoor.volume = audioSlider.value;
        jailDoorRigth.volume = audioSlider.value;
        knifeAudio.volume = audioSlider.value;
        bookBigBrown.volume = audioSlider.value;
        player.volume = audioSlider.value;
        phase1Enemy.volume = audioSlider.value;
        phase4Enemy.volume = audioSlider.value;
        enteryDoor.volume = audioSlider.value;
        torch.volume = audioSlider.value;
        hatch.volume = audioSlider.value;
        pickUp.volume = audioSlider.value;
        door1.volume = audioSlider.value;
        doorEndOfPh3.volume = audioSlider.value;
    }
}
