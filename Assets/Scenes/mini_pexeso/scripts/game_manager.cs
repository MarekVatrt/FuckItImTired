using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class game_manager : MonoBehaviour
{
    public bool first_card_flipped;
    public bool second_card_flipped;
    public Card_script first_card;
    public Card_script second_card;
    public int cards_clicked;
    public int score;

    //aby mal hrac cas si pozriet karty
    public bool is_checking = false;
    public float match_delay = 2.5f;

    void Start()
    {
        first_card_flipped=false;
        second_card_flipped=false;
        first_card=null;
        second_card=null;
        cards_clicked=0;
        score=0;
    }

    public void start_check()
    {
        //vratime sa ak sa akurat kontroluje match
        if (is_checking) return;
        
        is_checking = true;
        
        //zacneme kontrolu
        StartCoroutine(check_match_and_reset());
    }

    //najprv robene pomocou Update, ale to otoci druhu kartu instantne
    private IEnumerator check_match_and_reset()
    {
        //rutina zobrazi kartu na match_delay
        yield return new WaitForSeconds(match_delay); 

        if (first_card.card_id == second_card.card_id)
        {
            Debug.Log(first_card.card_id);
            Debug.Log(second_card.card_id);
            Debug.Log("yippee a match");
            
            //odstranime karty (nastavime front a back na false), nie samotnu kartu, lebo bz zmizla
            //ak by karta zmizla grid by sa automaticky rearangol
            first_card.transform.GetChild(0).gameObject.SetActive(false);
            first_card.transform.GetChild(1).gameObject.SetActive(false);
            //zaroven vypneme script aby sa na nu nedalo kliknut
            first_card.GetComponent<Card_script>().enabled=false;


            second_card.transform.GetChild(0).gameObject.SetActive(false);
            second_card.transform.GetChild(1).gameObject.SetActive(false);
            second_card.GetComponent<Card_script>().enabled=false;
            //second_card.gameObject.SetActive(false); 
            
            add_score();
        }
        else
        {
            Debug.Log("aw hell nah, gimme a match");
            
            //otocime naspat
            first_card.flip_back();
            second_card.flip_back();
        }

        //resetujeme vsetky flags
        first_card = null;
        second_card = null;
        first_card_flipped = false;
        second_card_flipped = false;
        cards_clicked = 0;
        is_checking = false;
    }

    void Update()
    {
        
    }

    void add_score()
    {
        score++;
        Debug.Log(score);
    }
}
