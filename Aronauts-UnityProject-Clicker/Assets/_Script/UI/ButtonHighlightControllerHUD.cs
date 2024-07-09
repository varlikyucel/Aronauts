using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlightControllerHUD : MonoBehaviour
{
    [SerializeField]
    private List<ButtonFeedbackHUD> buttons;

    [SerializeField]
    private AudioSource buttonClickSound;

    private ButtonFeedbackHUD currentlySelectedButton;

    // List to store special buttons
    [SerializeField]
    private List<string> specialButtonIds;
    private List<ButtonFeedbackHUD> specialButtons = new List<ButtonFeedbackHUD>();

    public delegate void ButtonClickedDelegate(ButtonFeedbackHUD clickedButton);
    public event ButtonClickedDelegate OnAnyButtonClicked;

    private void Awake()
    {
        if (buttons.Count == 0)
            buttons = new List<ButtonFeedbackHUD>(GetComponentsInChildren<ButtonFeedbackHUD>());

        foreach (var button in buttons)
        {
            button.OnClicked += () => ButtonClicked(button);

            // Check if the button is one of the special buttons
            if (specialButtonIds.Contains(button.ButtonId))
            {
                specialButtons.Add(button);
            }
        }
    }

    private void ButtonClicked(ButtonFeedbackHUD clickedButton)
    {
        // Check if clicked button is special
        bool isSpecialButtonClicked = specialButtons.Contains(clickedButton);

        // If a non-special button is clicked
        if (!isSpecialButtonClicked)
        {
            // Reset all buttons
            foreach (var button in buttons)
            {
                if (button != clickedButton)
                {
                    button.ResetToDefault();
                }
            }
        }
        else // If a special button is clicked
        {
            // Reset other special buttons if they are currently selected
            foreach (var specialButton in specialButtons)
            {
                if (specialButton != clickedButton && specialButton == currentlySelectedButton)
                {
                    specialButton.ResetToDefault();
                }
            }
        }

        // Update the currently selected button
        currentlySelectedButton = clickedButton;

        // Play the button click sound
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }

        // Invoke the delegate for button click
        OnAnyButtonClicked?.Invoke(clickedButton);
    }

    public void ResetAll()
    {
        foreach (var button in buttons)
        {
            button.ResetToDefault();
        }
        currentlySelectedButton = null;
    }

    public void ResetButtons()
    {
        ResetAll();
    }


}
