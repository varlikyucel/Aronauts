using UnityEngine;

public class ScaleAndRotate : MonoBehaviour
{
    public bool startTransform = false; // Flag to control when the transformation starts
    public float scaleSpeed = 0.5f; // Speed of scaling
    public float rotateSpeed = 50f; // Speed of rotation

    void Update()
    {
        if (startTransform)
        {
            // Scale the object up
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

            // Rotate the object around the Y axis
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }

    // Method to be called by the button's OnClick event
    public void StartTransformation()
    {
        startTransform = true;
    }
}
