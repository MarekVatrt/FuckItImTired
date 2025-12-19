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
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            playerInstance.transform.position = spawnPosition;
            // playerInstance.SetActive(true);
        }
    }
}
