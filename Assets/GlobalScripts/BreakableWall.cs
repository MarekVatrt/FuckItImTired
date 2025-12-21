using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableWall : MonoBehaviour
{
    [Header("Wall Stats")]
    public int maxHealth = 3;
    private int currentHealth;

    // public Tilemap tilemap;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer otherSprite;
    public Sprite[] damageStages; // 0 = intact, last = almost broken
    public GameObject breakParticles;

    private bool playerInRange = false;

    private Player_attack.weapon_struct playerWeapon;

    void Start()
    {
        if(QuestManager.Instance != null && QuestManager.Instance.IsAtStep(QuestStep.Completed))
        {
            otherSprite.gameObject.SetActive(true);
            // tilemap.ClearAllTiles();
            spriteRenderer.sprite = otherSprite.sprite;
            Destroy(spriteRenderer.gameObject);
            Destroy(gameObject);
        }
        currentHealth = maxHealth;
        if( Player_attack.Instance != null)
            playerWeapon = Player_attack.Instance.curr_weapon;
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown("space"))
        {
            TryBreak();
        }
    }

    void TryBreak()
    {
        // if (playerWeapon == null) return;

        int damage = playerWeapon.damage;
        currentHealth -= damage;

        UpdateVisuals();

        if (currentHealth <= 0)
        {
            BreakWall();
        }
    }

    void UpdateVisuals()
    {
        // if (damageStages.Length == 0) return;

        // int stageIndex = Mathf.Clamp(
        //     damageStages.Length - currentHealth - 1,
        //     0,
        //     damageStages.Length - 1
        // );

        // spriteRenderer.sprite = damageStages[stageIndex];
        float healthPercent = (float)currentHealth / maxHealth;
        spriteRenderer.color = new Color(1f, healthPercent, healthPercent);

    }

    void BreakWall()
    {
        if (breakParticles)
            Instantiate(breakParticles, transform.position, Quaternion.identity);

        Destroy(spriteRenderer.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if( Player_attack.Instance != null)
                playerWeapon = Player_attack.Instance.curr_weapon;
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // playerWeapon = null;
            playerInRange = false;
        }
    }
}
