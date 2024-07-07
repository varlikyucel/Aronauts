using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConstantRotation : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(0, 0, 1); // Z-axis for rotation
    public float rotationSpeed = 100f; // Rotation speed

    private Rigidbody rb;
    private Quaternion initialRotation; // To store the initial rotation

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Ensure gravity doesn't affect the rotation
        rb.isKinematic = false; // Must be false to use angular velocity

        initialRotation = transform.rotation; // Store the initial rotation
    }

    void OnEnable()
    {
        rb.WakeUp(); // Ensure the Rigidbody is awake
        transform.rotation = initialRotation; // Reset to the initial rotation
        ApplyRotation(); // Apply rotation
    }

    void OnDisable()
    {
        rb.angularVelocity = Vector3.zero; // Stops the rotation
        transform.rotation = initialRotation; // Optionally reset rotation to initial state
    }

    private void ApplyRotation()
    {
        // Convert rotation axis and speed to angular velocity
        rb.angularVelocity = rotationAxis.normalized * rotationSpeed;
    }
}
