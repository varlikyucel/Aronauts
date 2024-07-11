using UnityEngine;

public class GameController : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // Note: This will only work when running a built game. It won't work in the Unity editor.
        // To simulate quitting in the editor, you can use the following line (optional):
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
