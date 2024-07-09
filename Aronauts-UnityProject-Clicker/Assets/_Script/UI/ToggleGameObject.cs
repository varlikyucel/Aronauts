using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle; // Assign this in the Unity Editor
    private bool isObjectActive = false;

    private void Update()
    {
        // Check if the objectToToggle is inactive
        if (!objectToToggle.activeSelf && isObjectActive)
        {
            // Reset the button's state
            isObjectActive = false;
        }
    }

    public void ToggleObject()
    {
        isObjectActive = !isObjectActive;
        objectToToggle.SetActive(isObjectActive);
    }
}
