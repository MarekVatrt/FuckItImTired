using UnityEngine;
using System.Collections;

public class Enemy_ai : MonoBehaviour
{
    [SerializeField] Animator animator;
    new SpriteRenderer renderer;

    //weapon
    GameObject weapon_holder;
    //enemy variables
    public int damage;
    public float knockback;
    public float settable_walk_speed;
    private float walk_speed;
    //ak je player in range, attack
    public bool in_range;
    public int attack_direction;

    //hit variables
    public bool is_knocked_back;
    private bool is_attacking = false;
    
    //detection and chase variables
    private bool chasing;
    public float detection_distance;
    private Transform player;
    private GameObject exclamation;
    private CircleCollider2D vision_field;
    private Rigidbody2D body;
    void Start()
    {   
        walk_speed=settable_walk_speed;
        chasing=false;
        player=null;
        is_knocked_back=false;
        
        renderer=GetComponentInParent<SpriteRenderer>();
        vision_field = GetComponent<CircleCollider2D>();
        vision_field.radius = detection_distance;
        body=transform.root.GetComponent<Rigidbody2D>();
        //getneme weapon holder object
        weapon_holder=transform.root.GetChild(3).GetChild(0).gameObject;
        weapon_holder.SetActive(false);
        //getneme si exclamation object
        exclamation=transform.root.GetChild(4).gameObject;
        exclamation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!is_knocked_back)
        {
            if (chasing && player != null)
            {
                //Debug.Log("hybemee");
                Vector2 direction = ((Vector2)player.position - body.position).normalized;
                get_dominant_direction(direction);
                body.linearVelocity = direction * walk_speed;
                //nastavime animaciu podla vacsieho smeru po x-ovej osi
                if (direction.x > 0)
                {
                    animator.SetBool("is_walking",true);
                    renderer.flipX=true;
                }
                else if (direction.x < 0)
                {
                    animator.SetBool("is_walking",true);
                    renderer.flipX=false;
                }
                else
                {
                    animator.SetBool("is_walking",false);
                }
            }
            else
            {
                body.linearVelocity = Vector2.zero;
                animator.SetBool("is_walking",false);
            }
        }
        //ak je player v poli utoku a enemy este neutoci, nech zautoci
        if (in_range && !is_attacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    void get_dominant_direction(Vector2 direction)
    {
        //zistime ci je dominantne x alebo y (horizontal alebo vertical movement)
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                attack_direction = 1; 
            }
            else
            {
                attack_direction = 2; 
            }
        }
        else
        {
            if (direction.y > 0)
            {
                attack_direction = 3; 
            }
            else
            {
                attack_direction = 4; 
            }
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
    IEnumerator AttackSequence()
    {
        //nech sa funkcia nespusti viackrat
        is_attacking = true;
        //zapneme vykricnik=upozornime na utok

        //zastavime enemy
        //nastavime jeho rychlost na 0
        walk_speed=0;
        
        //nastavime umiestnenie zbrane a vykricniku
        set_weapon_orientation();
        set_exclamation_pos();
        //exclamation.transform.position = weapon_holder.transform.position;
        exclamation.SetActive(true);

        //pockame 0.8 sekund - dame hracovi cas na uhyb
        yield return new WaitForSeconds(0.8f);
        //vypneme vykricnik
        exclamation.SetActive(false);
        //zautoc
        weapon_holder.SetActive(true);
        
        //zbran tam bude 0.2 sekundy
        yield return new WaitForSeconds(0.2f);

        //vypneme zbran
        weapon_holder.SetActive(false);
        
        //moze zase utocit
        is_attacking = false;
        //obnovime jeho rychlost
        walk_speed=settable_walk_speed;
    }

    void set_weapon_orientation()
    {
        //utok hore,dole, doprava, dolava
        switch (attack_direction)
        {
            //doprava
            case 1:
                weapon_holder.transform.localPosition= new Vector3(0,0,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(0f, 0f, 0f);
                break;
            //dolava
            case 2:
                weapon_holder.transform.localPosition= new Vector3(0,0,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(0f, -180f, 0f);
                break;
            //hore
            case 3:
                weapon_holder.transform.localPosition= new Vector3(-0.23f,0.27f,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(0f, 0f, 86f);
                break;
            //dole
            case 4:
                weapon_holder.transform.localPosition= new Vector3(-0.23f,-0.1f,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(180f, 0f, 86f);
                break;
        }
        Debug.Log("orientacia je "+attack_direction);
    }

    void set_exclamation_pos()
    {
        switch (attack_direction)
        {
            //doprava
            case 1:
                exclamation.transform.localPosition= new Vector3(1,0,0);
                break;
            //dolava
            case 2:
                exclamation.transform.localPosition= new Vector3(-1,0,0);
                break;
            //hore
            case 3:
                exclamation.transform.localPosition= new Vector3(0,1,0);
                break;
            //dole
            case 4:
                exclamation.transform.localPosition= new Vector3(0,-1,0);
                break;
        }
         Debug.Log("orientacia je "+attack_direction);
    }
}
