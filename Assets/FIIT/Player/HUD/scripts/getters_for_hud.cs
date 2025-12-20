using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class getters_for_hud : MonoBehaviour
{
    //canvas objekty
    [SerializeField] private Slider healthbar;
    [SerializeField] private Slider attack_bar;
    [SerializeField] private TextMeshProUGUI health_text;
    [SerializeField] private Image weapon_image;

    //skripty
    private Player_attack attack_script;

    //lokalne data
    private float attack_freq;
    private GameObject last_checked_weapon_object;
    private PlayerStats stats;

    //na schovanie HUD (napr v minigames)
    public bool enable_hud;
    private Canvas HUD;

    //povodne bez vsetkych check-ov, potom som ich pridal vsade, lebo neustale hadzalo NullExepti..
    //pravdepodobne kvoli nacasovaniu vzniku weapon objektov a vzniku tohto scriptu zaroven
    //weapons sa nestihnu inicializovat skor ako sa spusti tento skript
    void Start()
    {

        HUD=GetComponent<Canvas>();
        //ak chceme skryt HUD
        //napr v nejakych minigames
        HUD.enabled=enable_hud;
        /*if (!enable_hud)
        {
            HUD.enabled=false;
        }
        else
        {
            HUD.enabled=true;
        }*/
        
        //gettneme canvas objekty
        healthbar=transform.GetChild(0).GetComponent<Slider>();
        attack_bar=transform.GetChild(1).GetComponent<Slider>();
        health_text=transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        weapon_image=transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>();

        //gettneme si skripty
        attack_script = transform.parent.GetComponentInChildren<Player_attack>();
        // health_script = transform.parent.GetComponentInChildren<take_damage_player>();
        stats = PlayerStats.Instance;

        //nastavime zakladne objekty v canvase pomocou hodnot zo skriptov
        if (healthbar != null && stats != null)
        {
            healthbar.maxValue = stats.MaxHealth;
            healthbar.minValue = 0;
            healthbar.value = stats.CurrentHealth;
            health_text.text = $"HP: {stats.CurrentHealth}/{stats.MaxHealth}";
        }


        if (attack_bar != null && attack_script != null)
        {
            attack_freq = attack_script.attack_freq;
            attack_bar.maxValue = attack_freq;
            attack_bar.minValue = 0;
        }

        //logiku nastavenia zbrane sme presunuli do vlastnej funkcie pre prehladnost
        update_weapon_hud();
    }

    void Update()
    {
        
        HUD.enabled=DialogueManager.Instance != null && !DialogueManager.Instance.DialogueActive();

        if (healthbar != null && stats != null)
        {
            healthbar.maxValue = stats.MaxHealth;
            healthbar.value = stats.CurrentHealth;
            health_text.text = $"HP: {stats.CurrentHealth}/{stats.MaxHealth}";
        }


        //kontrola zmeny zbrane
        if (attack_script.curr_weapon.weapon_object != last_checked_weapon_object)
        {
            update_weapon_hud();
        }

        //v pripade zmeny zbrane kontrola a nastavenie novych attack?bar hodnot
        if (attack_bar != null && attack_script != null)
        {
            //nastavenie freq
            attack_freq = attack_script.attack_freq;
            attack_bar.maxValue = attack_freq;
            //timer ide od attack_freq k 0, cize spravne naplnanie slideru ziskame takto
            attack_bar.value = attack_freq - attack_script.timer;

            //ak sme pripraveny na utok, handle attack_bar nastavime na gold
            if (attack_bar.handleRect != null)
            {
                //gettneme image handlu
                Image handleImage = attack_bar.handleRect.GetComponent<Image>();

                if (handleImage != null)
                {
                    //ak utok - gold
                    if (attack_bar.value == attack_freq)
                    {
                        handleImage.color = Color.gold;
                    }
                    //ak sa este nacitava - white
                    else
                    {
                        handleImage.color = Color.white;
                    }
                }
            }
        }
    }

    private void update_weapon_hud()
    {
        //kontroluje ci existuje curr_weapon, ak ano, priradi weapon_image jeho sprite
        if (weapon_image != null && attack_script.curr_weapon.weapon_object != null)
        {
            //zobereme sprite aktualnej selectnutej zbrane a ulozime ho
            SpriteRenderer sr = attack_script.curr_weapon.weapon_object.GetComponent<SpriteRenderer>();
            weapon_image.sprite = sr.sprite;
            last_checked_weapon_object = attack_script.curr_weapon.weapon_object;
        }
        //ak nie, weapon image sprite zostane null
        else if (weapon_image != null)
        {
            weapon_image.sprite = null;
        }
    }
}