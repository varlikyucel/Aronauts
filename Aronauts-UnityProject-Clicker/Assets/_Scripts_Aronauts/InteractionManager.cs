using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ObjectInteractionController currentSelectedObject;

    // Call this method to select an object
    public void SelectObject(ObjectInteractionController newSelectedObject)
    {
        if (currentSelectedObject != null && currentSelectedObject != newSelectedObject)
        {
            currentSelectedObject.ResetToDefaultMaterial(); // Reset the previous object to its default material
        }

        if (newSelectedObject != currentSelectedObject)
        {
            currentSelectedObject = newSelectedObject;
            currentSelectedObject.SetSelectedMaterial(); // Set the selected material on the new object
        }
    }

    // This method can be called from other scripts or triggered by mouse events
    public void HandleObjectSelection(ObjectInteractionController objectController)
    {
        SelectObject(objectController); // Select the object
    }
}
