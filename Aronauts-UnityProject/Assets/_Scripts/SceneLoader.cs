using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Public field to assign the scene name in the Unity Editor
    public float delayInSeconds = 5.0f; // Public field to adjust the delay before scene load

    // Method to be called by the button's OnClick event
    public void LoadSceneWithDelay()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(LoadSceneAfterDelay(sceneToLoad, delayInSeconds)); // Start the coroutine
        }
        else
        {
            Debug.LogError("Scene name is empty. Please assign a scene name in the inspector.");
        }
    }

    // Coroutine for loading the scene with a delay
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneName); // Load the scene
    }
}
