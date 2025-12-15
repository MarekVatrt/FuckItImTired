using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class grid_script : MonoBehaviour
{
    //predne strany karticiek
    public Sprite[] front_sprites;
    //scripty od karticiek, pouzijeme v nich fnkciu make_card
    //na randomne priradene id a sprite
    private Card_script[] card_scripts;
    void Start()
    {
        //gettneme vsetky card scripts
        card_scripts = GetComponentsInChildren<Card_script>();
        
        if (card_scripts.Length != 24)
        {
            Debug.LogError("mas zly pocet karticiek braski");
            return;
        }

        //vytvorime list s id 0,0,1,1,2,2..
        List<int> card_ids = new List<int>();
        for (int i = 0; i < 12; i++)
        {
            card_ids.Add(i); //0,1,2..
            card_ids.Add(i); //0,1,2..
        }

        //premiesame ich, aby na zaciatku kazdej hry boli nahodne rozmiestnene
        Shuffle(card_ids); 
        
        //ku kazdemu id priradime sprite, vytvorime pary karticiek
        for (int i = 0; i < card_scripts.Length; i++)
        {
            int assigned_id = card_ids[i];
            Sprite assigned_sprite = front_sprites[assigned_id];
            
            // Call a new method on your Card_script to set the ID and Sprite
            card_scripts[i].make_card(assigned_id, assigned_sprite);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
