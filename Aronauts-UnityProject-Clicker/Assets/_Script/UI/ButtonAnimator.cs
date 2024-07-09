using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For using Lists
using UnityEngine.UI;

public class UIButtonAnimator : MonoBehaviour
{
    public float moveDistance = 50f;  // Distance to move (in pixels)
    public float moveTime = 2f;       // Time to complete the move (in seconds)
    public List<GameObject> exceptionButtons; // Buttons that won't reset the animation

    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Coroutine currentMoveCoroutine;
    private bool isMoved;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        var buttonHighlightController = FindObjectOfType<ButtonHighlightController>();
        if (buttonHighlightController != null)
        {
            buttonHighlightController.OnAnyButtonClicked += HandleAnyButtonClicked;
        }
    }

    private void OnDestroy()
    {
        var buttonHighlightController = FindObjectOfType<ButtonHighlightController>();
        if (buttonHighlightController != null)
        {
            buttonHighlightController.OnAnyButtonClicked -= HandleAnyButtonClicked;
        }
    }

    public void OnButtonClick()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        if (isMoved)
        {
            // Move back to original position
            currentMoveCoroutine = StartCoroutine(MoveToPosition(originalPosition));
            isMoved = false;
        }
        else
        {
            // Move upwards
            Vector2 targetPosition = originalPosition + new Vector2(0, moveDistance);
            currentMoveCoroutine = StartCoroutine(MoveToPosition(targetPosition));
            isMoved = true;
        }
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition)
    {
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
    }

    private void HandleAnyButtonClicked(ButtonFeedback clickedButton)
    {
        // Check if the clicked button is an exception
        if (exceptionButtons.Contains(clickedButton.gameObject))
        {
            // If it's an exception, do nothing and return
            return;
        }

        if (clickedButton.gameObject != gameObject && isMoved)
        {
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }
            currentMoveCoroutine = StartCoroutine(MoveToPosition(originalPosition));
            isMoved = false;
        }
    }
}
