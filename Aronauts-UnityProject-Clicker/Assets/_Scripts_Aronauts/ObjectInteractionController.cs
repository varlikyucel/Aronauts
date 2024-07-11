using UnityEngine;
using System.Collections;

public class ObjectInteractionController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectedMaterial;

    public GameObject[] objectsToActivateOnSelection;
    public GameObject[] objectsToDeactivateOnSelection;
    public float activationDelay = 0.5f;  // Delay in seconds

    private MeshRenderer meshRenderer;
    private bool isSelected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResetToDefaultMaterial(); // Ensure the initial material is set correctly
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            meshRenderer.material = hoverMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    private void OnMouseDown()
    {
        InteractionManager interactionManager = FindObjectOfType<InteractionManager>();
        if (interactionManager != null)
        {
            interactionManager.HandleObjectSelection(this);
        }
    }

    public void ResetToDefaultMaterial()
    {
        isSelected = false;
        meshRenderer.material = defaultMaterial;

        // Start the coroutine to deactivate/activate objects after a delay
        StartCoroutine(ChangeObjectActivation(false));
    }

    public void SetSelectedMaterial()
    {
        isSelected = true;
        meshRenderer.material = selectedMaterial;

        // Start the coroutine to deactivate/activate objects after a delay
        StartCoroutine(ChangeObjectActivation(true));
    }

    private IEnumerator ChangeObjectActivation(bool isSelected)
    {
        yield return new WaitForSeconds(activationDelay); // Wait for the specified delay

        if (isSelected)
        {
            // Activate specified objects
            foreach (GameObject obj in objectsToActivateOnSelection)
            {
                obj.SetActive(true);
            }

            // Deactivate specified objects
            foreach (GameObject obj in objectsToDeactivateOnSelection)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            // Deactivate specified objects
            foreach (GameObject obj in objectsToActivateOnSelection)
            {
                obj.SetActive(false);
            }

            // Activate specified objects
            foreach (GameObject obj in objectsToDeactivateOnSelection)
            {
                obj.SetActive(true);
            }
        }
    }
}
