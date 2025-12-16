using UnityEngine;
using System.Collections;

public class Player_attack : MonoBehaviour
{
    //potrebujem script aby som zistil na ktoru stranu bol hrac naposledy otoceny
    //do tej strany zautoci
    Player_controller player_controller_script;
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


    public float attack_freq;
    public float timer;
    private int attack_orientation;
    
    void Start()
    {
        //getneme weapons objekt, co je rodic rodica konkretnej zbrane
        weapon_holder = transform.GetChild(0).gameObject;
        //ziskame skript controlleru
        player_controller_script=GetComponent<Player_controller>();
        attack_orientation=1;
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

        //nastavime orientaciu utoku
        set_weapon_orientation();

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
    void set_weapon_orientation()
    {
        attack_orientation=player_controller_script.facing_direction;
        //utok hore,dole, doprava, dolava
        switch (attack_orientation)
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
                weapon_holder.transform.localPosition= new Vector3(-0.38f,0.27f,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(0f, 0f, 86f);
                break;
            //dole
            case 4:
                weapon_holder.transform.localPosition= new Vector3(-0.38f,-0.1f,0);
                weapon_holder.transform.localRotation=Quaternion.Euler(180f, 0f, 86f);
                break;
        }
        Debug.Log("orientacia je "+attack_orientation);
    }
    
}
