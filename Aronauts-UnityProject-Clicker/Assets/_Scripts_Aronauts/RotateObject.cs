using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform objectToRotate; // The object that will be rotated
    public Vector3 rotationAxis = Vector3.up; // Default rotation around the Y-axis
    public float rotationDegree = 90.0f; // Degrees to rotate each time

    private bool triggerRotation = false; // To trigger rotation step

    void Update()
    {
        if (triggerRotation)
        {
            objectToRotate.Rotate(rotationAxis, rotationDegree);
            triggerRotation = false; // Reset trigger
        }
    }

    public void StartRotation()
    {
        triggerRotation = true;
    }

    public void StopRotation()
    {
        triggerRotation = false;
    }
}
