using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    // Add this method as an event handler for the button's click event in the Unity Editor.
    public void ResetLevel()
    {
        // Get the current scene's build index.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene to reset the level.
        SceneManager.LoadScene(currentSceneIndex);
    }
}
