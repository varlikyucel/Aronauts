using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform objectToMove; // Drag your object here in the inspector
    public float moveSpeed = 1.0f; // Adjust this value to change the speed
    public Vector3 moveDirection = Vector3.left; // Set default direction to left (negative x)

    private bool isMoving = false; // Flag to check if movement is ongoing

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            objectToMove.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    // Call this to start moving in the x direction
    public void StartMoving()
    {
        isMoving = true;
    }

    // Call this to stop moving
    public void StopMoving()
    {
        isMoving = false;
    }
}
