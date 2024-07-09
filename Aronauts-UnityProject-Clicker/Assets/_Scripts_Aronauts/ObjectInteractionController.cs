using UnityEngine;

public class ObjectInteractionController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectedMaterial;

    public GameObject[] objectsToActivateOnSelection;
    public GameObject[] objectsToDeactivateOnSelection;

    private MeshRenderer meshRenderer;
    private bool isSelected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResetToDefaultMaterial(); // Ensure the initial material is set correctly
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter called on " + gameObject.name);
        if (!isSelected)
        {
            meshRenderer.material = hoverMaterial;
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit called on " + gameObject.name);
        if (!isSelected)
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown called on " + gameObject.name);

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

    public void SetSelectedMaterial()
    {
        isSelected = true;
        meshRenderer.material = selectedMaterial;

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
}
