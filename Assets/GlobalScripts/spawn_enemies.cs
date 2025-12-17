using UnityEngine;

public class spawn_enemies : MonoBehaviour
{

    public GameObject enemy_prefab;

    //minimum enemies co sa spawnu v scene
    public int min_enemies_per_spawner = 1;

    //radius v ktorom sa budu okolo spawn pointu spawnovat
    public float spawn_radius = 3f;

    //neviem co by sa stalo keby enemies spawnem v objektoch s colliders
    //a radsej to ani nechcem vediet, tak sa tam spawnovat nebudu
    public LayerMask blocked_layers;

    //pred "zaciatkom" hry sa nebudu spawnovat
    public bool spawning_enabled;

    //lokacie spawnerov
    private Transform[] spawn_points;

    void Awake()
    {
        //gettneme lokacie "deti" - spawnerov
        spawn_points = GetComponentsInChildren<Transform>();

        //vyradime rodica zo spawn pointov
        // nevedel som ze getcomp.. zobere aj transform poziciu rodica tbh
        spawn_points = System.Array.FindAll(spawn_points, t => t != transform);
    }

    void Start()
    {
        //ak je zapnuty spawning, spawneme
        if (spawning_enabled)
            spawn();
    }

    void spawn()
    {
        //iterujeme cez spawnery
        foreach (Transform point in spawn_points)
        {
            //rand pocet
            int count = Random.Range(
                min_enemies_per_spawner,
                min_enemies_per_spawner + 1
            );

            //kazdemu enemy priradime poziciu
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = get_valid_position(point.position);
                Instantiate(enemy_prefab, pos, Quaternion.identity);
            }
        }
    }


    Vector3 get_valid_position(Vector3 center)
    {
        for (int i = 0; i < 20; i++)
        {
            //v radiuse spawneru najdeme rand poziciu
            Vector2 offset = Random.insideUnitCircle * spawn_radius;
            Vector3 pos = center + new Vector3(offset.x, 0f, offset.y);

            //kontrola ci nie je v blocked layers
            if (!Physics.CheckSphere(pos, 0.5f, blocked_layers))
                return pos;
        }

        //ak nenajdeme valid position, vratime poziciu stredu spawn radiusu
        return center;
    }
}
