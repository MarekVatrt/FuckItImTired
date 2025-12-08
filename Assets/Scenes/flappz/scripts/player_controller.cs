using UnityEngine;

public class player_controller : MonoBehaviour
{
    [SerializeField] private float jump_power=5f;
    private Rigidbody2D body;

    private Vector2 xy;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        xy= new Vector2(0,jump_power);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flap();
            //Debug.Log("flap");
        }
    }

    void flap()
    {
        body.linearVelocity = xy; 
        //body.linearVelocity.y=jump_power;
    }
}
