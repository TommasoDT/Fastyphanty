using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelancetta : MonoBehaviour
{
    // lancetta velocità 
    public GameObject auto;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.eulerAngles = new Vector3(0, 0, Mathf.Abs(auto.GetComponent<CarControls>().currentspeed.x) * -10);//muovi lancetta
        
    }
}
