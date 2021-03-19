using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Carsound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource sound;
    public AudioClip start;
    public AudioClip idle;
    public bool once;
    public float now;
    public bool outofuel;

    public bool sounds=true;//se la macchina è spenta non posso spegnerci i componenti perciò ecco la mia soluzione 
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        sound.clip = start;
        sound.loop = false;
        sound.Play();
        once = false;
        now = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       GetComponent<AudioSource>().enabled=sounds;
       GameObject.Find("Car").transform.GetChild(1).GetComponent<AudioSource>().enabled=sounds;
       GameObject.Find("Car").transform.GetChild(2).GetComponent<AudioSource>().enabled=sounds;
        if(Time.time>now+3 && once == false)
        {
            sound.clip = idle;
            sound.loop = true;
            sound.Play();
            once = true;
        }
        if (Time.time > now+3 && outofuel==false)
        {
            sound.pitch =Math.Abs(gameObject.GetComponent<CarControls>().BackWheel.GetComponent<Rigidbody2D>().angularVelocity / 600)+1;//backwheel as reference
        }
        if(outofuel==true)
        {
            sound.Stop();
        }
       
        
    }
    
}
