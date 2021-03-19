using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel_collector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectables")
        {
            if (collision.gameObject.name.Contains("Fuel"))
            {
                if (GetComponent<CarControls>().fuel <= 9000)
                { GetComponent<CarControls>().fuel += 1000; }
                else { GetComponent<CarControls>().fuel = 10000; }



            }
            Destroy(collision.gameObject);
           GetComponent<deathscripth>().GetComponent<predscripth>().gaspicked += 1;//incrementa il contatore del carburante preso
        }

    }
}
