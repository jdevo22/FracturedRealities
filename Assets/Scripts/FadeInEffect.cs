using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public float delayBeforeFade = 2f; // Time in seconds before the fade starts
    public float fadeDuration = 3f; // Duration of the fade effect
    public bool onStart = false;
    private Image fadeImage;

    void Start()
    {
        if (onStart)
        {
            fadeImage = GetComponent<Image>();
            StartCoroutine(FadeInWithDelay());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void transitionOverlay()
    {
        gameObject.SetActive(true);
        fadeImage = GetComponent<Image>();
        StartCoroutine(FadeInWithDelay());
    }

    IEnumerator FadeInWithDelay()
    {
        // Keep the screen black for the delay duration
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Fade in over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the image is fully transparent and disable it
        color.a = 0f;
        fadeImage.color = color;
        gameObject.SetActive(false);
    }
}
