using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int startingLevel = 0; // The initial level ID to start from.

    private void Start()
    {
        // Load the starting level when the script is initialized.
        LoadLevel(startingLevel);
    }

    public void LoadLevel(int levelID)
    {
        // Load the level by its build index (levelID).
        SceneManager.LoadScene(levelID);
    }

    public void LoadNextLevel()
    {
        // Load the next level based on the current scene's build index.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene exists.
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene.
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // You can handle reaching the end of the available levels here.
            Debug.LogWarning("No more levels available.");
        }
    }
}
