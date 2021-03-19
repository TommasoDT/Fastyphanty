using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
   
    public float spawnoffsetx=200;
    public float lastposition;
    //ultima posizione
    public GameObject fuel;
    
    
    public bool reached=true;
    public bool spawned=false;
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
        if(reached==true)//se la macchina ha raggiunto l'oggetto (reached viene acceso dall'oggetto fuel prima di autodistruggersi) setta la posizione attuale 
        {
            reached=false;spawned=false;
            lastposition=car.transform.position.x;
        }   
        if(car.transform.position.x>lastposition+spawnoffsetx&&spawned==false)//se la macchina ha superato la distanza bersaglio , chiama la funzone per spawnare l'oggetto fuel
            {    GameObject.Find("Floor").GetComponent<FloorGenerator>().Spawn(fuel, false,2,1);//Instantiate(fuel,posizione_oggetto,new Quaternion(0,0,0,0));
                spawned=true;
            }
           
    } 
        
}

