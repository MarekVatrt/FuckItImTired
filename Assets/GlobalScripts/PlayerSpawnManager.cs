using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Default Spawn Point")]
    [SerializeField] private Transform defaultSpawnPoint;

    private GameObject playerInstance;

    void Awake()
    {
        Debug.Log("SPAWN MAANGER AWAKE");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPosition = defaultSpawnPoint.position;

        // Check if returning from a minigame
        if (!string.IsNullOrEmpty(MinigameReturnData.ReturnScene))
        {

            spawnPosition = MinigameReturnData.ReturnPosition;
            Debug.Log("Returning player to previous scene spawn point.");
            
            MinigameReturnData.ReturnScene = null; // reset
        }
        

        // Instantiate player if not already in scene
        if (playerInstance == null)
        {
            Debug.Log("VYTVORIL SOM NOVEHO ADAMA");
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("NEVYTVORIL SOM noveho player");
            playerInstance.transform.position = spawnPosition;
            // playerInstance.SetActive(true);
        }

        Debug.Log($"spawn pos je nastavene na: {spawnPosition}");
    }
}
