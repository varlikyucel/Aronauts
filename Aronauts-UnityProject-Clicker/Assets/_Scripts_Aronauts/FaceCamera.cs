using UnityEngine;

public class FaceCameraWithOffset : MonoBehaviour
{
    private Camera mainCamera;

    [Tooltip("Offset to adjust the rotation in the Inspector.")]
    public Vector3 rotationOffset;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Calculate the direction to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Calculate the rotation to look at the camera
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation offset
            targetRotation *= Quaternion.Euler(rotationOffset);

            // Apply the rotation to the object
            transform.rotation = targetRotation;
        }
    }
}
