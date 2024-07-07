using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeFromToBlack : MonoBehaviour
{
    public Image fadeOverlay; // Assign the full-screen black Image in the inspector
    public float fadeDuration = 2.0f; // Duration of the fade effect

    // This method is automatically called at the start of the scene
    void Start()
    {
        // Start with fading from black to transparent
        StartCoroutine(FadeFromBlack());
    }

    // Coroutine to fade from black to transparent
    IEnumerator FadeFromBlack()
    {
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fadeOverlay.gameObject.SetActive(false); // Disable the overlay after fading to transparent
    }

    // Public method to start fading from transparent to black
    public void FadeToBlack()
    {
        fadeOverlay.gameObject.SetActive(true); // Ensure the overlay is active
        StartCoroutine(FadeToBlackCoroutine());
    }

    // Coroutine to fade from transparent to black
    IEnumerator FadeToBlackCoroutine()
    {
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        // Keep the overlay active after fading to black if needed
    }

    // Example method to be called by a UI Button to trigger the fade to black
    public void OnButtonClick()
    {
        FadeToBlack();
    }
}
