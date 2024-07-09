using UnityEngine;
using UnityEngine.UI;

public class ButtonActivationManager : MonoBehaviour
{
    public Button[] activationButtons; // Assign A, B, C buttons in Unity Editor
    public Button[] deactivationButtons; // Assign deactivation buttons here in Unity Editor
    public Button[] targetButtons; // Assign multiple target buttons in Unity Editor

    private CanvasGroup[] targetButtonCanvasGroups;
    private int lastActivatedButtonIndex = -1; // -1 indicates no button has been pressed

    void Start()
    {
        // Initialize CanvasGroups for each target button
        targetButtonCanvasGroups = new CanvasGroup[targetButtons.Length];
        for (int i = 0; i < targetButtons.Length; i++)
        {
            targetButtonCanvasGroups[i] = targetButtons[i].GetComponent<CanvasGroup>();
            if (targetButtonCanvasGroups[i] == null)
            {
                targetButtonCanvasGroups[i] = targetButtons[i].gameObject.AddComponent<CanvasGroup>();
            }
            targetButtonCanvasGroups[i].interactable = false;
            targetButtonCanvasGroups[i].blocksRaycasts = false;
        }

        // Add listeners to the activation buttons
        for (int i = 0; i < activationButtons.Length; i++)
        {
            int index = i; // Local copy of the index for the lambda capture
            activationButtons[i].onClick.AddListener(() => ActivationButtonClicked(index));
        }

        // Add listeners to the deactivation buttons
        foreach (Button btn in deactivationButtons)
        {
            btn.onClick.AddListener(() => SetAllTargetButtonsActive(false));
        }
    }

    private void ActivationButtonClicked(int index)
    {
        if (index == lastActivatedButtonIndex)
        {
            // If the same button is pressed again, deactivate all target buttons
            SetAllTargetButtonsActive(false);
            lastActivatedButtonIndex = -1; // Reset the last activated button index
        }
        else
        {
            // If a different button is pressed, activate all target buttons
            SetAllTargetButtonsActive(true);
            lastActivatedButtonIndex = index;
        }
    }

    private void SetAllTargetButtonsActive(bool isActive)
    {
        foreach (CanvasGroup canvasGroup in targetButtonCanvasGroups)
        {
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }

    private void OnDestroy()
    {
        // Remove listeners when the object is destroyed
        foreach (Button btn in activationButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
        foreach (Button btn in deactivationButtons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }
}
