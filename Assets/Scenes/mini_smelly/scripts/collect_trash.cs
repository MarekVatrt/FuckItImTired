using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class collect_trash : MonoBehaviour
{
    [SerializeField] private int trash_count;
    [SerializeField] private TextMeshProUGUI text_field;
    public int curr_trash;
    void Start()
    {
        curr_trash=0;
    }

    void Update()
    {
        text_field.text=curr_trash.ToString()+"/"+trash_count.ToString();
    }
}
