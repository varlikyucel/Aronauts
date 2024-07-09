using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonHighlightControllerMenuButtons : MonoBehaviour
{
    [SerializeField]
    private List<ButtonFeedbackMenuButtons> buttons;

    [SerializeField]
    private AudioSource buttonClickSound;

    public delegate void ButtonClickedDelegate(ButtonFeedbackMenuButtons clickedButton);
    public event ButtonClickedDelegate OnAnyButtonClicked;

    private void Awake()
    {
        // Initialize buttons list if empty
        if (buttons.Count == 0)
        {
            buttons = new List<ButtonFeedbackMenuButtons>(GetComponentsInChildren<ButtonFeedbackMenuButtons>());
        }

        // Subscribe to button click events
        foreach (var button in buttons)
        {
            button.OnClicked += () => ButtonClicked(button);
        }
    }

    private void ButtonClicked(ButtonFeedbackMenuButtons clickedButton)
    {
        // Reset all buttons except the clicked one
        foreach (var button in buttons)
        {
            if (button != clickedButton)
            {
                button.ResetToDefault();
            }
        }

        // Start a coroutine to reset the clicked button after a short delay
        StartCoroutine(ResetClickedButtonAfterDelay(clickedButton));

        // Play the button click sound
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }

        // Invoke the delegate for button click
        OnAnyButtonClicked?.Invoke(clickedButton);
    }

    private IEnumerator ResetClickedButtonAfterDelay(ButtonFeedbackMenuButtons clickedButton)
    {
        // Wait for a very short time before resetting the clicked button
        yield return new WaitForSeconds(0.1f);
        clickedButton.ResetToDefault();
    }

    public void ResetAll()
    {
        foreach (var button in buttons)
        {
            button.ResetToDefault();
        }
    }
}
