using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Player_controller : MonoBehaviour
{

    public static Player_controller Instance;

    [Header("Control modifiers")]
    public bool reverseHorizontal;
    public bool reverseVertical;
    // z playerstats objektu budeme ziskavat potrebne staty
    // nebudeme ich ale tuto modifikovat
    // ziskavat sa budu npr. stats.CurrentMoveSpeed

    private PlayerStats stats;

    //movement
    [SerializeField] private Animator animator;
    private Rigidbody2D body;
    private Vector2 xy;

    public int facing_direction;

    public bool is_knocked_back;

    // switch na vypnutie hybania pri "pause game"
    // pri inv, quest dialog atd..
    public static bool inputLocked;

    // potrebujeme hotbar a inventar kvoli activation hud
    Hotbar hotbar;
    HotbarUI hotbarUI;
    InventoryManager inventory;


    void Awake()
    {
        Instance = this;
        hotbar = GetComponentInChildren<Hotbar>();
        hotbarUI = FindFirstObjectByType<HotbarUI>();

    }

    // inicializuj pociatocne premenne 
    void Start()
    {
        facing_direction = 1;
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        inventory = InventoryManager.Instance;
    }


    void Update()
    {

        // Ak je hra pauznuta tak sa skipne cely update
        // znemoznime hracovi sa hybat
        
        if (inputLocked)
            return;


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (reverseHorizontal) x *= -1;
        if (reverseVertical) y *= -1;

        xy = new Vector2(x, y).normalized;

        HandleFacingAndAnimations(x, y);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            UseActiveHotbarItem();
        }

    }

    void HandleFacingAndAnimations(float x, float y)
    {
        //nastavime stranu, na ktoru bol naposledy obrateny player
        // pre attacking script, skopirovane a upravene z enemy_ai
        //zistil som ze lepsie je menit tu facing_diretion nizsie v kode, kde nacitavam aj animacie xd
        //get_dominant_direction(xy);
        if (x != 0)
        {
            if (x > 0)
            {
                facing_direction = 1;
                animator.SetBool("is_walking_right", true);
            }
            if (x < 0)
            {
                facing_direction = 2;
                animator.SetBool("is_walking_left", true);
            }
        }
        else
        {
            animator.SetBool("is_walking_left", false);
            animator.SetBool("is_walking_right", false);
        }
        if (y != 0)
        {
            if (y > 0)
            {
                facing_direction = 3;
                animator.SetBool("is_walking_up", true);
            }
            else if (y < 0)
            {
                facing_direction = 4;
                animator.SetBool("is_walking_down", true);
            }
        }
        else
        {
            animator.SetBool("is_walking_up", false);
            animator.SetBool("is_walking_down", false);
        }
    }

    void FixedUpdate()
    {
        //ak sme boli hitnuty, nachvilu zastavime, aby mohol prebehnut knockback
        if (!is_knocked_back)
        {
            Vector2 velocity = xy * stats.CurrentMoveSpeed;
            body.linearVelocity = velocity;
        }
    }

    void UseActiveHotbarItem()
    {
        ItemData item = hotbar.GetActiveItem();
        if (item != null)
        {
            if (inventory == null)
                Debug.Log("invenotry je NULL");
            inventory.UseItem(item, transform.root.gameObject);
        }

        hotbarUI.Refresh();
    }
}
