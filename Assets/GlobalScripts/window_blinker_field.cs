using UnityEngine;

public class window_blinker_field : MonoBehaviour
{
    new public SpriteRenderer renderer;
    //farba, ktoru budeme upravovat
    public Color color; 
    //nastavime range pre variables nizsie, aby nebol neobmedzeny
    [Range(0f, 1f)]
    public float max_alpha=0.2f;   
    public float blink_speed=0.3f;   

    void Update()
    {
        //pomocou pingpong pojdeme z 0 na 1 (0 je 0, 1 je v tomto pripade max_alpha)
        float alpha = Mathf.PingPong(Time.time * blink_speed, max_alpha);
        Color new_color = color;
        new_color.a = alpha;
        renderer.color = new_color;
    }
}
