using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ObjectToFollow;
    
    
    public  float Zooms = 0.02f;//velocità di raggiungimento dello zoom goal
    public  int Zoomstandard = 6;//zoom da fermo
    public float Zoomgoal = 0;//zoom da raggiungere
    public float Zoom = 6;//zoom della telecamera
    public bool audio;
    public float Zoomdivider = 4;//parametro di divisione della velocità , più basso è , maggiore sarà lo zoom relativo alla velocità

    //private float PreviousObjectSpeedOnX = 0;
    //private float PreviousObjectSpeedOnY = 0;

    private float ObjectSpeedOnX = 0;
    //private float ObjectSpeedOnY = 0;

    // Update is called once per frame
    void Update()
    { 
        
        /*
        ObjectSpeedOnX = ObjectToFollow.GetComponent<CarControls>().currentspeed.x;
        ObjectSpeedOnY = ObjectToFollow.GetComponent<CarControls>().currentspeed.y;

        if (PreviousObjectSpeedOnX < ObjectSpeedOnX && Zoom < MaxZoom)
            Zoom += PreviousObjectSpeedOnX * 0.005f;
        else if(PreviousObjectSpeedOnX > ObjectSpeedOnX && Zoom > MinZoom)
            Zoom -= PreviousObjectSpeedOnX * 0.005f;

        Camera.main.orthographicSize = Zoom + 5; //5 è il valore default della camera di Unity
        Camera.main.transform.position = new Vector3(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y, -10);

        PreviousObjectSpeedOnX = ObjectToFollow.GetComponent<CarControls>().currentspeed.x;
        PreviousObjectSpeedOnY = ObjectToFollow.GetComponent<CarControls>().currentspeed.y;
        */



        Camera.main.transform.position = new Vector3(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y+2, -10);//segue auto
        ObjectSpeedOnX = Mathf.Abs(ObjectToFollow.GetComponent<CarControls>().currentspeed.x);//Debug.Log(Zoomgoal);
        Zoomgoal= Zoomstandard+ (ObjectSpeedOnX/ Zoomdivider);
        if(Zoomgoal > Zoom) { Zoom += Zooms; }
        if(Zoomgoal < Zoom) { Zoom -= Zooms; }
        Camera.main.GetComponents<AudioSource>()[0].enabled=audio;
        Camera.main.GetComponents<AudioSource>()[1].enabled=audio;
        Camera.main.orthographicSize = Zoom ; //Zoom telecamera
        //Application.targetFrameRate = 60;
    }
}
