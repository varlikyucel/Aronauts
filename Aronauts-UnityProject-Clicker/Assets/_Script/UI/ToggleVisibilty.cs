using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    [System.Serializable]
    public class ButtonGameObjectPair
    {
        public ButtonFeedback button;
        public GameObject gameObject;
    }

    public List<ButtonGameObjectPair> buttonGameObjectPairs;
    public List<string> specialButtonIds; // List of IDs for special buttons

    private void Start()
    {
        // Subscribe to the OnClicked event of each ButtonFeedback
        ButtonFeedback[] allButtons = FindObjectsOfType<ButtonFeedback>();
        foreach (var buttonFeedback in allButtons)
        {
            buttonFeedback.OnClicked += () => OnAnyButtonClicked(buttonFeedback);
        }
    }

    private void OnAnyButtonClicked(ButtonFeedback clickedButton)
    {
        // Check if the clicked button is a special button
        if (specialButtonIds.Contains(clickedButton.ButtonId))
        {
            // Do nothing if it's a special button
            return;
        }

        foreach (var pair in buttonGameObjectPairs)
        {
            if (pair.button != null)
            {
                if (pair.button == clickedButton)
                {
                    // Toggle the GameObject of the clicked button
                    ToggleCanvasGroupVisibility(pair.gameObject);
                }
                else
                {
                    // Hide the GameObjects of other buttons
                    SetCanvasGroupVisibility(pair.gameObject, false);
                }
            }
        }
    }


    private void ToggleCanvasGroupVisibility(GameObject obj)
    {
        if (obj != null)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                bool isVisible = canvasGroup.alpha > 0;
                SetCanvasGroupVisibility(obj, !isVisible);
            }
        }
    }

    private void SetCanvasGroupVisibility(GameObject obj, bool isVisible)
    {
        if (obj != null)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = isVisible ? 1 : 0;
                canvasGroup.interactable = isVisible;
                canvasGroup.blocksRaycasts = isVisible;
            }
        }
    }
}
