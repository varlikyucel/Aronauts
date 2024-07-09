using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ButtonFeedbackHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Color highlightColor, clickedColor;

    [SerializeField]
    private float fadeDuration = 0.1f;

    private bool selected = false;

    private Color defaultColor;

    [SerializeField]
    private bool changeColorOnClick = true;

    [SerializeField]
    private Vector3 scaleUpValue = new Vector3(1.1f, 1.1f, 1.1f);

    [SerializeField]
    private string buttonId; // Unique identifier for the button

    public event Action OnClicked;

    // Public property to access the ButtonId
    public string ButtonId
    {
        get { return buttonId; }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        defaultColor = button.image.color;
    }

    public void ResetButton()
    {
        if (!selected)
        {
            button.image.DOColor(defaultColor, fadeDuration);
        }
        button.transform.localScale = Vector3.one;
    }

    public void ResetToDefault()
    {
        // Reset the selected flag
        selected = false;

        // Reset the button's visual state to default
        button.image.color = defaultColor;
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
        {
            button.image.DOColor(highlightColor, fadeDuration);
            transform.DOScale(scaleUpValue, fadeDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            ResetButton();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClicked?.Invoke();

        // Toggle the button state
        selected = !selected;

        if (selected && changeColorOnClick)
        {
            ApplyClickedFeedback();
        }
        else
        {
            ResetButton();
        }
    }

    public void ApplyClickedFeedback()
    {
        button.image.DOColor(clickedColor, fadeDuration);
        button.transform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        if (button == null)
            return;

        button.image.DOKill();
        button.transform.DOKill();
        button.image.color = defaultColor;
        button.transform.localScale = Vector3.one;
    }
}
