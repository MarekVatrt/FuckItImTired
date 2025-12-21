using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public DeathScreenFade deathScreen;
    // public GameObject deathPanel;
    public Transform respawnPoint;
    private GameObject player;
    private PlayerStats stats;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = PlayerStats.Instance;
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        deathScreen.FadeIn();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        deathScreen.FadeOut();
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;

        player.transform.position = respawnPoint.position;
        stats.RestoreHealth();
    }
    
    // public void RespawnPlayer()
    // {
    //     Time.timeScale = 1f;
    //     deathPanel.SetActive(false);

    //     player.transform.position = respawnPoint.position;

    //     stats.RestoreHealth();
    // }
}
