using UnityEngine;

public class destroy_object : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("deletion_border"))
        {
            Destroy(gameObject);
        }
    }
}
