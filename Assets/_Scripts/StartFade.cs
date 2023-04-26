using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
    public float delayTime = 2f; // Delay before the fade-in effect starts in seconds
    public float fadeDuration = 1f; // Duration of the fade-in effect in seconds
    public Image blackStarterScreen; // Reference to the black UI image

    void Start()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the delay time

        float startTime = Time.time;
        float alpha = 1f;

        // Loop until the alpha value reaches 0
        while (alpha > 0f)
        {
            alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeDuration);
            blackStarterScreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Disable the panel or image once the fade-in effect is complete
        gameObject.SetActive(false);
    }
}
