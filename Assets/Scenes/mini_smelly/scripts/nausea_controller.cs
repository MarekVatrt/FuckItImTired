using UnityEngine;
using UnityEngine.UI;

public class nausea_controller : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private float fill_value;
    private float timer;
    public bool fill_or_empty;
    void Start()
    {
        fill_or_empty=true;
        timer=5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fill_or_empty)
        {
            timer=5f;
            slider.value += fill_value * Time.deltaTime;

            if (slider.value >= 100)
            {
                die_of_smell();
            }
        }
        //ak otvorime okno, nachvilu nausia pojde prec
        else
        {
            Debug.Log("sme tu");
            slider.value -= fill_value * Time.deltaTime;
            if (slider.value < 0)
            {
                slider.value=0;
            }
            timer-=Time.deltaTime;
            //po vyprsani casu sa okno zatvori
            if (timer <= 0)
            {
                timer=5f;
                fill_or_empty=true;
            }
        }
        
    }

    void die_of_smell()
    {
        Debug.Log("smrad itickarov bol na teba priliz moc");
    }
}
