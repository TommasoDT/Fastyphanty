using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudscript : MonoBehaviour
{
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            car.transform.position.x, car.transform.position.y, 1);//segue la macchina
    }
}
