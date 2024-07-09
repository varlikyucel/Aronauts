using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform objectToMove; // Drag your object here in the inspector
    public float moveIncrementInCm = 1.0f; // Move increment in centimeters
    public Vector3 moveDirection = Vector3.left; // Set default direction to left (negative x)

    // Method to move the object incrementally
    public void MoveIncrementally()
    {
        if (objectToMove != null)
        {
            float moveIncrementInMeters = moveIncrementInCm / 100.0f; // Convert cm to meters
            Vector3 oldPosition = objectToMove.position;
            objectToMove.position += moveDirection * moveIncrementInMeters;
            Debug.Log($"Object moved from: {oldPosition} to: {objectToMove.position} (Increment: {moveIncrementInCm} cm)");
        }
        else
        {
            Debug.LogError("objectToMove is not assigned in the Inspector.");
        }
    }
}
