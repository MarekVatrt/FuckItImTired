using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawned_object;
    [SerializeField] private float spawn_rate=0.5f;
    [SerializeField] private float spawn_height=6;
    private float timer;

    void Start()
    {
        timer=spawn_rate;
    }
    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            spawn_object();
            timer=spawn_rate;
        }
    }

    void spawn_object()
    {
        int object_index= Random.Range(0, spawned_object.Length);
        float x_pos = Random.Range(-7, 7); 
        GameObject instance = Instantiate(
            spawned_object[object_index],
            //random x pozicia + mimo view (y=7)
            new Vector3(x_pos,spawn_height,0),
            Quaternion.identity //no rotation
        );
    }
}
