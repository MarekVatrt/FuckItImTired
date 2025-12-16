using System.Linq.Expressions;
using UnityEngine;

//upravena kopia take_damage v enemy objekte
public class take_damage_player : MonoBehaviour
{
    //gettick attacked variables
    private int damage_taken;
    private float knockback_taken;
    public float knockback_timer = 0;
    private Vector2 enemy_position;

    private Rigidbody2D body;
    private Enemy_ai ai_script;
    private Player_controller controller_script;
    // Z player stats budeme tahat health
    private PlayerStats stats;




    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        controller_script = GetComponent<Player_controller>();
        stats = GetComponent<PlayerStats>();

        if (stats == null)
        {
            Debug.LogError("PlayerStats missing on Player!");
        }
    }

    void Update()
    {
        if (knockback_timer > 0)
        {
            knockback_timer -= Time.deltaTime;
            if (knockback_timer <= 0)
            {
                knockback_timer = 0;
                controller_script.is_knocked_back = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("enemy_weapon"))
            return;

        Debug.Log("I have been hit oof");
        ai_script = other.transform.root.GetComponentInChildren<Enemy_ai>();
        if (ai_script == null)
            return;
        enemy_position = other.transform.position;

        damage_taken = ai_script.damage;
        knockback_taken = ai_script.knockback;

        controller_script.is_knocked_back = true;

        ApplyAttack();

    }
    // void take_attack()
    // {
    //     Vector2 knock_dir = ((Vector2)transform.position - enemy_position).normalized;
    //     body.AddForce(knock_dir * knockback_taken, ForceMode2D.Impulse);
    //     Debug.Log("Knockback Value: " + knockback_taken);

    //     knockback_timer = 0.2f;

    //     curr_health -= damage_taken;
    //     Debug.Log("ouch-player");
    //     if (curr_health <= 0)
    //     {
    //         death();
    //     }
    // }

    void ApplyAttack()
    {
        // Knockback
        Vector2 knock_dir = ((Vector2)transform.position - enemy_position).normalized;
        body.AddForce(knock_dir * knockback_taken, ForceMode2D.Impulse);

        knockback_timer = 0.2f;

        // HEALTH goes through PlayerStats
        stats.TakeDamage(damage_taken);
    }

    void death()
    {
        Debug.Log("curse uuu fiitkaris");
        Destroy(transform.root.gameObject);
    }
}
