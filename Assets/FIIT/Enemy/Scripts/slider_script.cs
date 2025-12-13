using UnityEngine;
using UnityEngine.UI;

public class slider_script : MonoBehaviour
{
    int max_health;
    int curr_health;
    take_damage take_damage_script;
    [SerializeField]Slider slider;
    void Start()
    {
        //zoberieme skript take_damage, potrebujeme max a curr health hodnoty
        take_damage_script=transform.root.GetComponentInChildren<take_damage>();

        //nastavime hodnoty pre slider
        slider.maxValue=take_damage_script.max_health;
        slider.minValue=0;
        slider.value=take_damage_script.curr_health;
    }

    void Update()
    {
        //updatujeme value
        slider.value = take_damage_script.curr_health;
    }
}
