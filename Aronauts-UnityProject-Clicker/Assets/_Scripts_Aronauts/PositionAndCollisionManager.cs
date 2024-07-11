using UnityEngine;
using System.Collections.Generic;

public class PositionAndCollisionManager : MonoBehaviour
{
    public AudioClip errorSound; // Assign this in the Inspector
    private AudioSource audioSource;

    private List<Vector3> safePositions = new List<Vector3>();
    private static PositionAndCollisionManager lastMovedObject;

    void Start()
    {
        // Initialize by saving the starting position as a safe state
        AddSafeState(transform.position);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the GameObject!");
        }

        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No collider found on the GameObject!");
        }
        else
        {
            collider.isTrigger = true; // Ensure the collider is set to trigger
            Debug.Log("Collider set to trigger mode.");
        }
    }

    private void OnMouseDown()
    {
        // Select this object when it is clicked
        lastMovedObject = this;
        Debug.Log("Object selected: " + gameObject.name);
    }

    public void OnButtonPress()
    {
        // Save the current position before any action
        AddSafeState(transform.position);
        Debug.Log("Button pressed. State saved and ready to detect collisions.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by {other.gameObject.name} with tag {other.tag}.");
        if (other.CompareTag("Obstacle") && lastMovedObject == this)
        {
            Debug.Log($"Trigger collision detected with {other.name}. Attempting to revert to a safe position.");
            PlayErrorSound();
            RevertToPreviousSafePosition();
        }
    }

    private void AddSafeState(Vector3 position)
    {
        safePositions.Add(position);
        Debug.Log($"Position saved at: {position}");
    }

    private void RevertToPreviousSafePosition()
    {
        if (safePositions.Count > 1) // Check if there is more than one saved state
        {
            // Remove the last state because it's the collision point
            safePositions.RemoveAt(safePositions.Count - 1);

            // Revert to the new last state
            Vector3 lastSafePosition = safePositions[safePositions.Count - 1];
            transform.position = lastSafePosition;

            Debug.Log($"Reverted to earlier safe state: {lastSafePosition}");
        }
        else
        {
            Debug.LogWarning("Attempted to revert, but no previous safe state was available.");
        }
    }

    private void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.PlayOneShot(errorSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or errorSound not set.");
        }
    }
}
