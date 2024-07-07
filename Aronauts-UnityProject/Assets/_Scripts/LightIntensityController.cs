using System.Collections;
using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight; // Assign your directional light in the inspector
    public float targetIntensity = 1.0f; // Target intensity to reach
    public float duration = 5.0f; // Duration over which the light intensity increases

    void Start()
    {
        if (directionalLight != null)
        {
            StartCoroutine(GraduallyIncreaseIntensity());
        }
    }

    IEnumerator GraduallyIncreaseIntensity()
    {
        float startIntensity = 0.0f; // Starting intensity
        float time = 0.0f;

        while (time < duration)
        {
            // Gradually increase the light's intensity over the duration
            directionalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            time += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the intensity is set to the target value at the end
        directionalLight.intensity = targetIntensity;
    }
}
