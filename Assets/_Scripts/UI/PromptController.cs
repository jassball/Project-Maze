using System.Collections;
using UnityEngine;
using TMPro;

public class PromptController : MonoBehaviour
{
    public float displayDuration = 1f;
    public float fadeInDuration = 3f;
    public float fadeOutDuration = 3f;

    private TextMeshProUGUI textMeshPro;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
        fadeCoroutine = StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade in
        float currentTime = 0f;
        Color startColor = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0f);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            textMeshPro.color = Color.Lerp(startColor, endColor, currentTime / fadeInDuration);
            yield return null;
        }

        // Display for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        currentTime = 0f;
        startColor = textMeshPro.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            textMeshPro.color = Color.Lerp(startColor, endColor, currentTime / fadeOutDuration);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
