using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public string scene_name;
    //Scene_changer scene_changer;

    void Start()
    {
    }

    //kedze elevator buttons nepouzivaju player script, zavolame ho zo skriptu buttons
    public void change_scene()
    {
        Scene_changer.Instance.from_elevator(scene_name);
    }
}
