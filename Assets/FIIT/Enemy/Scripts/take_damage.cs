using System.Linq.Expressions;
using UnityEngine;

public class take_damage : MonoBehaviour
{
    
    private int damage_taken;
    private float knockback_taken;
    private Vector2 player_position;
    public int health;
    public Rigidbody2D body;

    void Start()
    {
        body=transform.root.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            Player_attack weapons_script = other.transform.root.GetComponent<Player_attack>();
            player_position=other.transform.position;

            damage_taken=weapons_script.curr_weapon.damage;
            knockback_taken=weapons_script.curr_weapon.knockback;

            take_attack(player_position);
        }
    }
    void take_attack(Vector2 knock_from)
    {
        Vector2 knock_dir = ((Vector2)transform.position - player_position).normalized;
        body.linearVelocity = Vector2.zero;               
        body.AddForce(knock_dir * knockback_taken, ForceMode2D.Impulse);

        health-=damage_taken;
        Debug.Log("ouch");
        if (health <= 0)
        {
            death();
        }
    }
    void death()
    {
        Debug.Log("curse uuu fiitkaris");
        Destroy(transform.root.gameObject);
    }
}
