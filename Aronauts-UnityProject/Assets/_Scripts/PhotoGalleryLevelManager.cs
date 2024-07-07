using UnityEngine;
using UnityEngine.SceneManagement; // Needed to access scene information
using System.Linq;

public class PhotoGalleryLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelGallery
    {
        public string levelName;
        public GameObject gallery;
    }

    public LevelGallery[] levelGalleries; // Assign in the inspector

    // This method is called when the button is clicked
    public void ActivateCurrentLevelGallery()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SetLevel(currentLevelName);
    }

    public void SetLevel(string levelName)
    {
        // Deactivate all galleries
        foreach (var levelGallery in levelGalleries)
        {
            if (levelGallery.gallery != null) // Check if the gallery GameObject is assigned
                levelGallery.gallery.SetActive(false);
        }

        // Find and activate the gallery corresponding to the levelName
        var levelToActivate = levelGalleries.FirstOrDefault(lg => lg.levelName == levelName);
        if (levelToActivate != null && levelToActivate.gallery != null)
        {
            levelToActivate.gallery.SetActive(true);
        }
        else
        {
            Debug.LogWarning("LevelManager: No gallery found for level " + levelName);
        }
    }
}
