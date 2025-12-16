using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public string scene_name;

    public void change_scene()
    {
        SceneManager.LoadScene(scene_name);
    }
}
