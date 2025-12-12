using UnityEngine;

public class Enemy_ai : MonoBehaviour
{
    public int damage;
    public int health;
    public float detection_distance;
    private CircleCollider2D vision_field;
    void Start()
    {
        vision_field = GetComponent<CircleCollider2D>();
        vision_field.radius = detection_distance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("i see u fiit boai");
        }
    }


}
