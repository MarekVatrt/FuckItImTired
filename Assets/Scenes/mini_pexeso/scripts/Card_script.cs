using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card_script : MonoBehaviour, IPointerClickHandler
{
    private Image front;
    private Image back;
    public int card_id;
    game_manager game_script;

    //museli sme zmenit Start na Awake kvoli scriptu grid_script, ktory inicializoval karticky a hadzal NullRefference..
    void Awake()
    {
        //ziskame script na logiku hry
        game_script=transform.parent.GetComponent<game_manager>();
        back=transform.GetChild(0).GetComponent<Image>();
        front=transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        
    }

    /*void OnMouseDown()
    {
        Debug.Log("clicked");
        front.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
    }*/

    //OnMouseDown mi nefungoval na UI, tak som pouzil tuto funkciu
    public void OnPointerClick(PointerEventData eventData)
    {
        //ak sa prave checkuje par kariet, nedovolime otocenie
        if (game_script.is_checking) return; 
        

        Debug.Log("clickedd");
        front.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
        if (game_script.cards_clicked == 0)
        {
            game_script.first_card_flipped=true;
            game_script.first_card=this;
            game_script.cards_clicked++;
        }
        else if(game_script.cards_clicked == 1)
        {
            game_script.second_card_flipped=true;
            game_script.second_card=this;
            game_script.cards_clicked++;
            //ak sme otocili 2 karty, zacneme check
            game_script.start_check();
        }
        else
        {
            Debug.Log("Huh?");
        }
        
    }

    public void make_card(int id, Sprite sprite)
    {
        card_id = id;
        front.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        //front.sprite = sprite;
        
        //schovame front, aktivujeme back
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }

    public void flip_back()
    {
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }

}
