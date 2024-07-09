using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChildObjectDeactivator : MonoBehaviour
{
    public float delay = 2.0f; // The delay in seconds before deactivating the child objects.
    public List<GameObject> childObjects; // List of child objects you want to deactivate.
    public float fadeDuration = 1.0f; // Duration of the fade-out effect in seconds.

    private List<CanvasGroup> canvasGroups = new List<CanvasGroup>(); // References to the CanvasGroup components.
    private bool isFadingOut = false; // Flag to track the fade-out state.
    private float fadeStartTime; // Time when the fade-out effect started.

    private void Start()
    {
        // Ensure that at least one child object is assigned.
        if (childObjects == null || childObjects.Count == 0)
        {
            Debug.LogError("No child objects assigned.");
            enabled = false; // Disable the script to prevent errors.
            return;
        }

        // Initialize the CanvasGroup references for all child objects.
        foreach (GameObject obj in childObjects)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogError("Child object does not have a CanvasGroup component: " + obj.name);
                enabled = false; // Disable the script to prevent errors.
                return;
            }
            canvasGroups.Add(canvasGroup);

            // Ensure that the child objects are initially active and fully opaque.
            obj.SetActive(true);
            canvasGroup.alpha = 1f;
        }

        // Start a coroutine to deactivate the child objects with a fade-out effect after the specified delay.
        StartCoroutine(DeactivateChildrenWithFadeOut());
    }

    private IEnumerator DeactivateChildrenWithFadeOut()
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Reset the fade-out effect parameters.
        isFadingOut = true;
        fadeStartTime = Time.time;

        // Gradually decrease the opacity of all CanvasGroups to 0 (fully transparent).
        while (isFadingOut)
        {
            float elapsedTime = Time.time - fadeStartTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                canvasGroup.alpha = alpha;
            }

            if (alpha <= 0f)
            {
                isFadingOut = false; // Stop fading when fully transparent.
            }

            yield return null;
        }

        // Deactivate the child objects after the fade-out effect.
        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(false);
        }
    }
}
