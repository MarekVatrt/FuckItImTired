using UnityEngine;
using System.Collections;

public class Player_controller : MonoBehaviour{

    //characteristics
    private float move_speed=3f;

    //movement
    [SerializeField] private Animator animator;
    private Rigidbody2D body;
    private Vector2 xy;

    
    void Start(){
        body=GetComponent<Rigidbody2D>();
    }

    void Update(){

        //move, TODO asi prerobit nech to hned nacitava do vectora
        float x=Input.GetAxisRaw("Horizontal");
        float y=Input.GetAxisRaw("Vertical");

        xy= new Vector2(x,y).normalized;

        if(x!=0){
            if(x>0){
                animator.SetBool("is_walking_right",true);
            }
            if(x<0){
                animator.SetBool("is_walking_left",true);
            }
        }
        else{
            animator.SetBool("is_walking_left",false);
            animator.SetBool("is_walking_right",false);
        }
        if(y!=0){
            if(y>0){
                animator.SetBool("is_walking_up",true);
            }
            else if(y<0){
                animator.SetBool("is_walking_down",true);
            }
        }
        else{
            animator.SetBool("is_walking_up",false);
            animator.SetBool("is_walking_down",false);
        }
        

        //attack
        if(Input.GetKeyDown("space")){
            Attack();
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
        Vector2 velocity = xy * move_speed;
        body.linearVelocity = velocity; 
    }

    void Attack(){
        Debug.Log("attackkkkkkkkk");
    }

    void Interact(){
        Debug.Log("interacted");
    }

    void Inventory(){
        Debug.Log("Opened inv");
    }
}
