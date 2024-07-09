using UnityEngine;
using UnityEngine.Events;

public class MouseInteractionEvents : MonoBehaviour
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform> { }

    public TransformEvent onMouseEnter;
    public TransformEvent onMouseExit;
    public TransformEvent onMouseClick;

    public Camera cam; // Public camera variable to assign in Inspector

    private Transform previousHit = null;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Automatically find the main camera if not assigned
            if (cam == null)
            {
                Debug.LogError("No camera assigned to MouseInteractionEvents script and no Main Camera found.");
                return; // Do not proceed if no camera is assigned or found
            }
        }
    }

    void Update()
    {
        if (cam == null)
        {
            Debug.LogWarning("Camera is null, exiting update loop.");
            return; // Do not proceed if no camera is assigned or found
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        bool isHit = Physics.Raycast(ray, out hit);

        if (isHit)
        {
            if (previousHit != hit.transform)
            {
                if (previousHit != null)
                {
                    Debug.Log("Mouse exited: " + previousHit.name);
                    onMouseExit.Invoke(previousHit);
                }
                if (hit.transform == transform)
                {
                    Debug.Log("Mouse entered: " + hit.transform.name);
                    onMouseEnter.Invoke(hit.transform);
                }
                previousHit = hit.transform;
            }

            if (Input.GetMouseButtonDown(0) && hit.transform == transform)
            {
                Debug.Log("Mouse clicked: " + hit.transform.name);
                onMouseClick.Invoke(hit.transform); // Only invoke click if actually hitting the attached object
            }
        }
        else
        {
            if (previousHit != null)
            {
                Debug.Log("Mouse exited: " + previousHit.name);
                onMouseExit.Invoke(previousHit);
                previousHit = null;
            }
        }
    }
}
