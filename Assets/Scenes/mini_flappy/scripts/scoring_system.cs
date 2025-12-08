using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class scoring_system : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_field;
    public int score;
    void Start()
    {
        score=0;
    }
    void Update()
    {
        text_field.text=score.ToString();
    }

    void OnTriggerEnter2D(Collider2D triggered_border)
    {
        if (triggered_border.CompareTag("score_point"))
        {
            score+=1;
            Debug.Log("+point");
        }
        else if (triggered_border.CompareTag("deletion_border"))
        {
            Debug.Log("game over broski");
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("basket"))
        {
            Debug.Log("pole game over broskir");
        }
    }

    
}
