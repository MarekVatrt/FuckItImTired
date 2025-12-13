using UnityEngine;
using System.Collections;

public class Player_attack : MonoBehaviour
{
    //potrebujem script aby som zistil na ktoru stranu bol hrac naposledy otoceny
    //do tej strany zautoci
    Player_controller player_main_script;
    //budeme flipovat weapons object
    GameObject weapon_holder;
    
    [System.Serializable] public struct weapon_struct
    {
        public GameObject weapon_object;
        public float attack_freq;
        public int damage;
        public float knockback;
        public bool is_acq;
    }

    public weapon_struct[] weapons;
    public weapon_struct curr_weapon;
    private int weapon_index;


    private float attack_freq;
    private float timer;
    //false - vlavo, true - vpravo
    private bool attack_side;
    
    void Start()
    {
        //getneme weapons objekt, co je rodic rodica konkretnej zbrane
        weapon_holder = transform.GetChild(0).gameObject;
        //ziskame skript controlleru
        player_main_script=GetComponent<Player_controller>();
        attack_side=true;
        timer=0;

        //vypneme vsetky zbrane
        foreach(weapon_struct weapon in weapons)
        {
            weapon.weapon_object.SetActive(false);
        }

        weapon_index=0;
        curr_weapon = weapons[weapon_index];
        attack_freq=curr_weapon.attack_freq;
    }

    void Update()
    {
        //nastavime stranu utoku
        attack_side=player_main_script.is_facing_right;
        timer-=Time.deltaTime;
        if (timer < 0)
        {
            timer=0;
        }
        //po 0.2 sekundy zbran zmizne
        if (timer <= attack_freq-0.2)
        {
            curr_weapon.weapon_object.SetActive(false);
        }

        if (Input.GetKeyDown("space"))
        {
            //timer na frekvenciu utoku
            if (timer == 0)
            {
                Attack();
            }
        }
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("In k");
            Equip_weapon();
        }
    }

    void Attack()
    {
        Debug.Log("uttttooooook");

        timer=attack_freq;

        //zmena strany a rotacie podla posledneho stlacenia pohybu (A/D alebo </>)
        if (attack_side)
        {
            weapon_holder.transform.rotation=Quaternion.Euler(0,0,0); 
        }
        else
        {
            weapon_holder.transform.rotation=Quaternion.Euler(0,-180,0);
        }

        curr_weapon.weapon_object.SetActive(true);
    }
    void Equip_weapon()
    {
        curr_weapon.weapon_object.SetActive(false);
        curr_weapon=default;

        weapon_index++;
        if (weapon_index >= weapons.Length){
            weapon_index=0;
        }
        while (weapons[weapon_index].is_acq != true)
        {
            Debug.Log("In while");
            weapon_index++;
            if (weapon_index >= weapons.Length){
                weapon_index=0;
            }
        }

        Debug.Log("on weapon index: "+weapon_index);
        curr_weapon=weapons[weapon_index];
        curr_weapon.weapon_object.SetActive(false);
        attack_freq=curr_weapon.attack_freq;
        
    }
    
}
