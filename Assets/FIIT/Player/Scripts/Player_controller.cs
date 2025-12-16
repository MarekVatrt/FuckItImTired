using UnityEngine;
using System.Collections;

public class Player_controller : MonoBehaviour{

    //characteristics
    [SerializeField]private float move_speed=5.5f;

    //movement
    [SerializeField] private Animator animator;
    private Rigidbody2D body;
    private Vector2 xy;

    public int facing_direction;

    public bool is_knocked_back;

    
    void Start(){
        facing_direction=1;
        body=GetComponent<Rigidbody2D>();
    }

    void Update(){

        float x=Input.GetAxisRaw("Horizontal");
        float y=Input.GetAxisRaw("Vertical");

        xy= new Vector2(x,y).normalized;

        //nastavime stranu, na ktoru bol naposledy obrateny player
        // pre attacking script, skopirovane a upravene z enemy_ai
        //zistil som ze lepsie je menit tu facing_diretion nizsie v kode, kde nacitavam aj animacie xd
        //get_dominant_direction(xy);

        if(x!=0){
            if(x>0){
                facing_direction=1;
                animator.SetBool("is_walking_right",true);
            }
            if(x<0){
                facing_direction=2;
                animator.SetBool("is_walking_left",true);
            }
        }
        else{
            animator.SetBool("is_walking_left",false);
            animator.SetBool("is_walking_right",false);
        }
        if(y!=0){
            if(y>0){
                facing_direction=3;
                animator.SetBool("is_walking_up",true);
            }
            else if(y<0){
                facing_direction=4;
                animator.SetBool("is_walking_down",true);
            }
        }
        else{
            animator.SetBool("is_walking_up",false);
            animator.SetBool("is_walking_down",false);
        }
        

        //interact
        if(Input.GetKeyDown("e")){
            Interact();
        }

        if(Input.GetKeyDown("i")){
            Inventory();
        }

    }

    void FixedUpdate()
    {
        //ak sme boli hitnuty, nachvilu zastavime, aby mohol prebehnut knockback
        if (!is_knocked_back)
        {
            Vector2 velocity = xy * move_speed;
            body.linearVelocity = velocity; 
        }
    }

    void Interact(){
        Debug.Log("interacted");
    }

    void Inventory(){
        Debug.Log("Opened inv");
    }
}
