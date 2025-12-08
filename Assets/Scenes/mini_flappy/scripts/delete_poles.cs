using UnityEngine;

public class delete_poles : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject to_be_deleted_pole=collision.collider.gameObject;
        if (to_be_deleted_pole.CompareTag("basket"))
        {
            Destroy(to_be_deleted_pole);
        }
    }
}
