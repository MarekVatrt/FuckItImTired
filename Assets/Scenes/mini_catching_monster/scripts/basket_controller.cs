using UnityEngine;

public class basket_controller : MonoBehaviour
{
    private Rigidbody2D basket;
    private Vector2 xy;
    [SerializeField]private float move_speed=5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basket=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x=Input.GetAxisRaw("Horizontal");
        float y=0;

        xy= new Vector2(x,y).normalized;
    }

    void FixedUpdate()
    {
        Vector2 velocity= xy*move_speed;
        basket.linearVelocity = velocity;
    }
}
