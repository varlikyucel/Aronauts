using UnityEngine;

public class SliceController : MonoBehaviour
{
    public Transform slicePlane;

    void Update()
    {
        if (slicePlane != null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 slicePos = slicePlane.position;
                Vector3 sliceNormal = slicePlane.up; // Assuming 'up' is the slicing direction

                // Iterate through all materials
                foreach (Material mat in renderer.materials)
                {
                    mat.SetVector("_SlicePosition", new Vector4(slicePos.x, slicePos.y, slicePos.z, 0));
                    mat.SetVector("_SliceNormal", new Vector4(sliceNormal.x, sliceNormal.y, sliceNormal.z, 0));
                }
            }
        }
    }
}
