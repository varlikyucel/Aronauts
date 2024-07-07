using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpecificActivation : MonoBehaviour
{
    public string specificLevelName; // The name of the specific level where the GameObject should be deactivated
    // Alternatively, you can use public int specificLevelIndex; for build index

    void Start()
    {
        // Check if the current scene (level) name matches the specific level name
        if (SceneManager.GetActiveScene().name == specificLevelName)
        {
            // Deactivate the GameObject if we are in the specific level
            gameObject.SetActive(false);
        }
        else
        {
            // Activate the GameObject if we are not in the specific level
            gameObject.SetActive(true);
        }

        // If using build index, the condition would look like this:
        // if (SceneManager.GetActiveScene().buildIndex == specificLevelIndex)
    }
}
