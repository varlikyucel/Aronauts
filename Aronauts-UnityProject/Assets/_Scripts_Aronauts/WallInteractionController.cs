using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallInteractionController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectedMaterial;
    public GameObject gimbal; // Assign this in the inspector

    private MeshRenderer meshRenderer;
    private InteractionManager interactionManager;

    // Indicates whether this wall is currently selected
    private bool isSelected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (!isSelected)  // Only change to hover material if not selected
        {
            meshRenderer.material = hoverMaterial;
        }
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (!isSelected)  // Only reset to default if not selected
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        isSelected = true;  // Mark as selected
        interactionManager.SelectWall(this);
        interactionManager.ActivateGimbal(gimbal);
    }

    public void ResetToDefaultMaterial()
    {
        isSelected = false;  // Mark as not selected
        meshRenderer.material = defaultMaterial;
    }

    public void SetSelectedMaterial()
    {
        meshRenderer.material = selectedMaterial;
    }
}
