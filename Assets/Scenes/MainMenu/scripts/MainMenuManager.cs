using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        // Load first gameplay scene
        // SceneManager.LoadScene("GameplayScene"); // Replace with our starting scene
    }

    public void OnLoadGameClicked()
    {
        // Optional: Implement your load system here
        Debug.Log("Load game clicked");
    }

    public void OnSettingsClicked()
    {
        // Open settings panel
        Debug.Log("Settings clicked");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit clicked");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
