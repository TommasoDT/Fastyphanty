using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecarblancetta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    GameObject fuelv;
     float f;

    void Awake()
    {
        fuelv = GameObject.Find("Fuel");
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f= car.GetComponent<CarControls>().fuel;//componente fuel
        f=100*f/(car.GetComponent<CarControls>().maxfuel);
        f=Mathf.Round(f);
        f =(f*-1.5f)-30;//formula per trasformarlo in angolo
        transform.eulerAngles = new Vector3(0, 0, f);//ruota lancetta
    }
}
