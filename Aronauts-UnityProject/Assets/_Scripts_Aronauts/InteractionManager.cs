using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private GameObject currentActiveGimbal;
    private WallInteractionController currentSelectedWall;

    public void ActivateGimbal(GameObject newGimbal)
    {
        if (currentActiveGimbal != null)
        {
            currentActiveGimbal.SetActive(false);
        }

        currentActiveGimbal = newGimbal;
        currentActiveGimbal.SetActive(true);
    }

    public void SelectWall(WallInteractionController newSelectedWall)
    {
        if (currentSelectedWall != null && currentSelectedWall != newSelectedWall)
        {
            currentSelectedWall.ResetToDefaultMaterial();
        }

        if (newSelectedWall != currentSelectedWall)
        {
            currentSelectedWall = newSelectedWall;
            currentSelectedWall.SetSelectedMaterial();
        }
    }
}
