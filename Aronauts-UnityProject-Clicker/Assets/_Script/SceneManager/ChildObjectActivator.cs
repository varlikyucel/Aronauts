using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChildObjectActivator : MonoBehaviour
{
    public float delay = 2.0f; // The delay in seconds before activating the child objects.
    public List<GameObject> childObjects; // List of child objects you want to activate.
    public float fadeDuration = 1.0f; // Duration of the fade-in effect in seconds.

    private List<CanvasGroup> canvasGroups = new List<CanvasGroup>(); // References to the CanvasGroup components.
    private bool isFadingIn = false; // Flag to track the fade-in state.
    private float fadeStartTime; // Time when the fade-in effect started.

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

            // Deactivate the child objects and set their alpha to 0 initially (fully transparent).
            obj.SetActive(false);
            canvasGroup.alpha = 0f;
        }

        // Start a coroutine to activate the child objects with a fade-in effect after the specified delay.
        StartCoroutine(ActivateChildrenWithFadeIn());
    }

    private IEnumerator ActivateChildrenWithFadeIn()
    {
        // Wait for the specified delay.
        yield return new WaitForSeconds(delay);

        // Activate the child objects.
        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(true);
        }

        // Reset the fade-in effect parameters.
        isFadingIn = true;
        fadeStartTime = Time.time;

        // Gradually increase the opacity of all CanvasGroups to 1 (fully opaque).
        while (isFadingIn)
        {
            float elapsedTime = Time.time - fadeStartTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                canvasGroup.alpha = alpha;
            }

            if (alpha >= 1f)
            {
                isFadingIn = false; // Stop fading when fully opaque.
            }

            yield return null;
        }
    }
}
