using System.Linq.Expressions;
using UnityEngine;

public class take_damage : MonoBehaviour
{
    //gettick attacked variables
    private int damage_taken;
    private float knockback_taken;
    private Vector2 player_position;
    public int max_health;
    public int curr_health;
    private Rigidbody2D body;
    //refference of vision script - zastavenie movement pre pridanie knockbacku
    private Enemy_ai vision_field_script;
    //v skripte na enemy ai sa neustale nastavuje linearVelocity nove, takze knockback nefunguje
    //linearVelocity teda nachvilu vypneme aby sa pouzil knockback
    public float knockback_timer = 0;



    void Start()
    {
        curr_health = max_health;
        body = transform.root.GetComponent<Rigidbody2D>();
        vision_field_script = transform.parent.GetComponentInChildren<Enemy_ai>();
    }

    void Update()
    {
        if (knockback_timer > 0)
        {
            knockback_timer -= Time.deltaTime;
            if (knockback_timer <= 0)
            {
                knockback_timer = 0;
                vision_field_script.is_knocked_back = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weapon"))
        {
            Player_attack weapons_script = other.transform.root.GetComponent<Player_attack>();
            player_position = other.transform.position;

            damage_taken = weapons_script.GetCurrentDamage();

            // knockback_taken=weapons_script.curr_weapon.knockback;
            knockback_taken = weapons_script.GetCurrentKnockback();

            take_attack();
        }
    }
    void take_attack()
    {
        //nastavime na true, aby nahananie playera v enemy_ai neoverwritlo knockback
        vision_field_script.is_knocked_back = true;
        knockback_timer = 0.2f;

        Vector2 knock_dir = ((Vector2)transform.position - player_position).normalized;
        body.linearVelocity = Vector2.zero;
        body.AddForce(knock_dir * knockback_taken, ForceMode2D.Impulse);

        curr_health -= damage_taken;
        Debug.Log("ouch");
        if (curr_health <= 0)
        {
            death();
        }
    }
    void death()
    {
        EnemyDropTable dropTable = transform.root.GetComponent<EnemyDropTable>();
        if (dropTable != null)
            dropTable.Drop();

        Destroy(transform.root.gameObject);
    }

}
