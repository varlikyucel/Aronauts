using UnityEngine;
using System.Collections;

public class MaterialInteractionController : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;
    public Material selectedMaterial;
    public float selectedDuration = 1.0f;

    private MeshRenderer meshRenderer;
    private bool isSelected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResetToDefaultMaterial(); // Ensure the initial material is set correctly
    }

    private void OnMouseEnter()
    {
        //Debug.Log("OnMouseEnter called on " + gameObject.name);
        if (!isSelected)
        {
            meshRenderer.material = hoverMaterial;
        }
    }

    private void OnMouseExit()
    {
        //Debug.Log("OnMouseExit called on " + gameObject.name);
        if (!isSelected)
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown called on " + gameObject.name);
        if (!isSelected)
        {
            StartCoroutine(ShowSelectedMaterial());
        }
    }

    private IEnumerator ShowSelectedMaterial()
    {
        isSelected = true;
        meshRenderer.material = selectedMaterial;
        yield return new WaitForSeconds(selectedDuration);
        ResetToDefaultMaterial();
        isSelected = false;
    }

    public void ResetToDefaultMaterial()
    {
        meshRenderer.material = defaultMaterial;
    }
}
