using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    void Start()
    {
        
        GameManager.Instance.respawnPoint = transform;
    }
}
