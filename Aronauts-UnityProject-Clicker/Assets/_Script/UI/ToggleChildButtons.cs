using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleChildObjects : MonoBehaviour
{
    public CanvasGroup[] childObjects; // Assign specific child CanvasGroups in Unity Editor
    public Button[] specificButtons; // Assign specific buttons that will change opacity of children
    public float delayInSeconds = 1.0f; // Delay in seconds before toggling the children

    private bool areChildrenVisible = false;

    void Start()
    {
        // Add a click event listener if this is attached to a button
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => StartCoroutine(ToggleChildrenAfterDelay()));
        }

        // Add listeners to the specific buttons
        foreach (Button btn in specificButtons)
        {
            btn.onClick.AddListener(CloseChildren); // Deactivate children and update main state when these buttons are clicked
        }

        // Ensure specified child objects are initially invisible
        SetChildrenOpacity(0f);
    }

    private IEnumerator ToggleChildrenAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        ToggleChildren();
    }

    private void ToggleChildren()
    {
        areChildrenVisible = !areChildrenVisible;
        SetChildrenOpacity(areChildrenVisible ? 1f : 0f);
    }

    private void CloseChildren()
    {
        if (areChildrenVisible)
        {
            areChildrenVisible = false;
            SetChildrenOpacity(0f);
        }
    }

    private void SetChildrenOpacity(float opacity)
    {
        foreach (CanvasGroup child in childObjects)
        {
            if (child != null)
            {
                child.alpha = opacity;
                child.interactable = opacity > 0;
                child.blocksRaycasts = opacity > 0;
            }
        }
    }
}
