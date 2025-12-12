using UnityEngine;
using System.Collections;

public class Player_attack : MonoBehaviour
{
    
    [System.Serializable] public struct weapon_struct
    {
        public GameObject weapon_object;
        public float attack_freq;
        public int damage;
        public bool is_acq;
    }

    public weapon_struct[] weapons;
    private weapon_struct curr_weapon;
    private int weapon_index;

    private float attack_freq;
    private float timer;
    
    void Start()
    {
        timer=0;

        //vypneme vsetky zbrane
        foreach(weapon_struct weapon in weapons)
        {
            weapon.weapon_object.SetActive(false);
        }

        weapon_index=0;
        curr_weapon = weapons[weapon_index];
        curr_weapon.weapon_object.transform.localScale = Vector3.zero;
        curr_weapon.weapon_object.SetActive(true); 
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
            curr_weapon.weapon_object.transform.localScale = Vector3.zero;
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
        curr_weapon.weapon_object.transform.localScale = new Vector3(1,1,1);

        /*
        Collider2D[] hit_ememies = Physics2D.OverlapCircleAll(curr_weapon.weapon_object.transform.position, 1, 2);
        foreach(Collider2D enemy in hit_ememies)
        {
            Debug.Log(enemy.name);
        }*/

    }

    void Equip_weapon()
    {
        curr_weapon.weapon_object.transform.localScale = Vector3.zero;
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
        curr_weapon.weapon_object.transform.localScale = Vector3.zero;
        curr_weapon.weapon_object.SetActive(true);
        attack_freq=curr_weapon.attack_freq;
        
    }
    
}
