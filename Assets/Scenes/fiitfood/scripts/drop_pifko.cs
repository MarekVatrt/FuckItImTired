using UnityEngine;

public class drop_pifko : MonoBehaviour
{
    //variable na detekciu ci je player v interactable zone
    bool is_in;
    void Start()
    {
        is_in=false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            is_in=true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            is_in=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //ak player stlaci E, dropni pifko
        if (Input.GetKeyDown("e") && is_in)
        {
            Debug.Log("fuck ye beer man");
            //enemydroptable prebrate z Assets/Enemy/scripts - pouzivane na dropy z enemies
            EnemyDropTable dropTable = GetComponent<EnemyDropTable>();
            if (dropTable != null)
            dropTable.Drop();
            else
            {
                Debug.Log("this shit empty yeet");
            }
        }
    }

}
