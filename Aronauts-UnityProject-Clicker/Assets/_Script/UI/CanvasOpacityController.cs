using UnityEngine;
using System.Collections;

public class CanvasOpacityController : MonoBehaviour
{
    public GameObject targetGameObject;
    public float fadeDuration = 0.5f; // Duration of the fade effect
    public float delay = 1.0f; // Delay before the fade starts

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Ensure the target GameObject has a CanvasGroup, add one if it doesn't
        canvasGroup = targetGameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = targetGameObject.AddComponent<CanvasGroup>();
        }
    }

    public void ToggleGameObject()
    {
        if (targetGameObject.activeSelf)
        {
            // Start fade out if GameObject is currently active
            StartCoroutine(FadeCanvasGroup(false, fadeDuration, delay));
        }
        else
        {
            // Activate the GameObject and start fade in
            targetGameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(true, fadeDuration, delay));
        }
    }

    private IEnumerator FadeCanvasGroup(bool fadeIn, float duration, float delayTime)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        float targetOpacity = fadeIn ? 1.0f : 0.0f; // Target opacity based on fade in or out
        float startOpacity = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / duration; // 0 to 1
            canvasGroup.alpha = Mathf.Lerp(startOpacity, targetOpacity, normalizedTime);
            yield return null; // Wait for the next frame
        }

        canvasGroup.alpha = targetOpacity;

        // Deactivate the GameObject after fade out
        if (!fadeIn)
        {
            targetGameObject.SetActive(false);
        }
    }
}
