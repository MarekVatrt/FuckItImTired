using UnityEngine;

public class Enemy_ai : MonoBehaviour
{
    public int damage;
    public float detection_distance;
    public float walk_speed;
    private bool chasing;
    private Transform player;
    private CircleCollider2D vision_field;
    private Transform enemyRoot;
    private Rigidbody2D body;
    void Start()
    {
        chasing=false;
        player=null;
        vision_field = GetComponent<CircleCollider2D>();
        vision_field.radius = detection_distance;
        body=transform.root.GetComponent<Rigidbody2D>();
        //enemyRoot = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (chasing && player != null)
        {
            enemyRoot.position = Vector2.MoveTowards(
                enemyRoot.position,
                player.position,
                walk_speed * Time.deltaTime
            );
        }*/
    }

    void FixedUpdate()
    {
        
        if (chasing && player != null)
        {
            Vector2 direction = ((Vector2)player.position - body.position).normalized;
            body.linearVelocity = direction * walk_speed;
        }
        else
        {
            body.linearVelocity = Vector2.zero;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("i see u fiit boai");
            chasing=true;
            player=other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("u can get away this time buckaroo");
            chasing=false;
            player=null;
        }
    }
}
