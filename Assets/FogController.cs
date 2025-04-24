using System.Collections;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [Header("Fog Settings")]
    public float fadeDuration = 5f;         // How long it takes to fade out
    public float startDelay = 0f;           // Optional delay before fade starts
    public float targetFogEndDistance = 1000f; // How far fog should end at the end of fade

    private float initialFogEndDistance;

    void Start()
    {
        // Ensure fog is enabled
        RenderSettings.fog = true;

        // Store the initial end distance so we can fade from it
        initialFogEndDistance = RenderSettings.fogEndDistance;

        StartCoroutine(FadeFogOut());
    }

    IEnumerator FadeFogOut()
    {
        // Optional delay
        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            // Interpolate the fog end distance
            RenderSettings.fogEndDistance = Mathf.Lerp(initialFogEndDistance, targetFogEndDistance, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final value is set
        RenderSettings.fogEndDistance = targetFogEndDistance;

        Debug.Log("Fog fade out complete.");
    }
}
