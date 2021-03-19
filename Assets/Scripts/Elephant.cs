using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    public Vector2 car;
    float maxX=20, maxY=20;
    float minX=1, minY=3;
    void Start()
    {
        target = GameObject.Find("target");
    }

    // Update is called once per frame
    void Update()
    {
        car = GameObject.Find("Car").GetComponent<CarControls>().currentspeed;
        target.transform.localPosition = new Vector2(  Tools.Clamp<float>(-car[0]/6 + maxX / 4, minX, maxX),
                                                       Tools.Clamp<float>(-car[1] + maxY / 3, minY, maxY)  );              
    }
}
