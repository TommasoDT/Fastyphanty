using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_spawner : MonoBehaviour
{
    public GameObject nuvola;
    GameObject car;
    public int spawn_distance = 20;
    float prev_spawn=30;
    public float maxY;
    public float minY;
    public float Xoffset;     
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("Car");      
    }

    // Update is called once per frame
    void Update()
    {
        maxY = Camera.main.transform.position.y + 4 * Camera.main.transform.localScale[1];
        minY = Camera.main.transform.position.y - 4 * Camera.main.transform.localScale[1];
        if (car.transform.position.x-spawn_distance>prev_spawn)
        {
            Instantiate(nuvola, new Vector2(car.transform.position.x+Xoffset,Random.Range(minY,maxY)), new Quaternion(0, 0, 0, 0));
            prev_spawn = car.transform.position.x + spawn_distance;
        }                            
    }
}
