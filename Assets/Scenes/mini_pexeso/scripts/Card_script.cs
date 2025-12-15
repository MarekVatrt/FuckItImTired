using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card_script : MonoBehaviour, IPointerClickHandler
{
    private Image front;
    private Image back;
    private int card_id;
    //museli sme zmenit Start na Awake kvoli scriptu grid_script, ktory inicializoval karticky a hadzal NullRefference..
    void Awake()
    {
        back=transform.GetChild(0).GetComponent<Image>();
        front=transform.GetChild(1).GetComponent<Image>();
        front.gameObject.SetActive(false);
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
        Debug.Log("clickedd");
        front.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
    }

    public void make_card(int id, Sprite sprite)
    {
        card_id = id;
        front.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        //front.sprite = sprite;
        
        //preistotu schovame front opat
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
    }
}
