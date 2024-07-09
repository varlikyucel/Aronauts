using System.Collections.Generic;
using UnityEngine;

public class ButtonHighlightController : MonoBehaviour
{
    [SerializeField]
    private List<ButtonFeedback> buttons;

    [SerializeField]
    private AudioSource buttonClickSound;

    private ButtonFeedback currentlySelectedButton;

    // List to store special buttons
    [SerializeField]
    private List<string> specialButtonIds;
    private List<ButtonFeedback> specialButtons = new List<ButtonFeedback>();

    public delegate void ButtonClickedDelegate(ButtonFeedback clickedButton);
    public event ButtonClickedDelegate OnAnyButtonClicked;

    // For destroy mode buttons
    public ButtonHighlightControllerDestroyMode otherButtonController;

    private void Awake()
    {
        if (buttons.Count == 0)
            buttons = new List<ButtonFeedback>(GetComponentsInChildren<ButtonFeedback>());

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

    private void ButtonClicked(ButtonFeedback clickedButton)
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

            // To reset destroy mode buttons
            otherButtonController.ResetButtons();
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
}
