using UnityEngine;
using UnityEngine.UI; // Required for the Button type
using UnityEngine.Events;

public class UIPlacementController : MonoBehaviour
{
    public UnityEvent<int> OnObjectSelected;
    public UnityEvent OnUndoRequested, OnMoveRequest, OnResetMovementButton, OnCancelPlacement, OnMovementStateEntered;

    public Button[] stopPlacementButtons; // Array of buttons that will stop placement mode

    private bool isPlacementModeActive = false;
    private int lastSelectedIndex = -1; // Initialize to an invalid index

    void Start()
    {
        // Add listeners to the stop placement buttons
        foreach (Button button in stopPlacementButtons)
        {
            button.onClick.AddListener(StopPlacementMode);
        }
    }

    public void ToggleObjectPlacement(int index)
    {
        if (isPlacementModeActive && index == lastSelectedIndex)
        {
            // If the same button is clicked again, exit placement mode
            CancelPlacementRequested();
            isPlacementModeActive = false;
            lastSelectedIndex = -1; // Reset the last selected index
        }
        else
        {
            // If a different button is clicked, switch to that placement mode
            SelectObjectWithIndex(index);
            isPlacementModeActive = true;
            lastSelectedIndex = index; // Update the last selected index
        }
    }

    public void SelectObjectWithIndex(int index)
    {
        OnObjectSelected?.Invoke(index);
    }

    public void RequestUndo()
    {
        OnUndoRequested?.Invoke();
    }

    public void MoveRequest()
    {
        OnMoveRequest?.Invoke();
    }

    public void ResetMovementButton()
    {
        OnResetMovementButton?.Invoke();
    }

    public void CancelPlacementRequested()
    {
        OnCancelPlacement?.Invoke();
    }

    public void EnterMovementState()
    {
        OnMovementStateEntered?.Invoke();
    }

    private void StopPlacementMode()
    {
        // Stop the placement mode
        CancelPlacementRequested();
        isPlacementModeActive = false;
        lastSelectedIndex = -1;
    }

    private void OnDestroy()
    {
        // Clean up listeners when the object is destroyed
        foreach (Button button in stopPlacementButtons)
        {
            button.onClick.RemoveListener(StopPlacementMode);
        }
    }
}
