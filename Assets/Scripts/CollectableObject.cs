using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CollectableObject : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject roadObject;
    private bool touchedRoad = false;
    GameObject car;
    GameObject self;
    GameObject Wheel_Front;
    GameObject Wheel_Back;
    public int despownoffset=10;
    public AudioClip pop;
    
    
    void Start()
    {
        self = this.gameObject;
        roadObject = GameObject.Find("Floor").transform.GetChild(0).gameObject;
        self.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -.1f));
        car = GameObject.Find("Car");
        Wheel_Front = GameObject.Find("Wheel_Front");
        Wheel_Back = GameObject.Find("Wheel_Back");
        //gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    void Update()
    {

        if (car.transform.position.x > self.transform.position.x + despownoffset && !Tools.isInCameraXView(Camera.main, self.transform.position))
        {
            GameObject.Find("Spawner").GetComponent<CollectObSpawner>().reached = true; Destroy(self);
        }

        //se la macchina tocca il carburante...ò
        if(self.GetComponent<BoxCollider2D>().IsTouching(car.GetComponent<BoxCollider2D>())||
        self.GetComponent<BoxCollider2D>().IsTouching(car.transform.GetChild(1).GetComponent<CircleCollider2D>())||
        self.GetComponent<BoxCollider2D>().IsTouching(car.transform.GetChild(2).GetComponent<CircleCollider2D>()))
        { 
            car.GetComponent<CarControls>().carburante();//funzione per incrementare il carburo
            GameObject.Find("death overlay").GetComponent<predscripth>().gaspicked+=1;//incrementa il contatore del carburante preso
            GameObject.Find("Spawner").GetComponent<CollectObSpawner>().reached=true;//setta a true una variabile dello spawner
            car.transform.GetChild(2).GetComponent<AudioSource>().Play();
            Destroy(self);//si distrugge
        }
    }
    
    
}
