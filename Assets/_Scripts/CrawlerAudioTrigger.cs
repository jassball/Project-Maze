using System.Collections;
using UnityEngine;

public class CrawlerAudioTrigger : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    private BoxCollider boxCollider;
    private AudioSource audioSource;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsPlayer) != 0)
        {
            StartCoroutine(PlayAudioAndDisableCollider());
        }
    }

    IEnumerator PlayAudioAndDisableCollider()
    {
        audioSource.Play();
        boxCollider.enabled = false;

        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }
}