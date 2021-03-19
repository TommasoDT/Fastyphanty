using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class car : MonoBehaviour
{
    public GameObject BackWheel;
    public GameObject FrontWheel;
    public GameObject veicle;
    public float currentspeed;
    public float speedF = 0;//piu che altro torque
    public float speedR = 0;
    public int Maxspeed = 200;
    public double Brake = 0.005;
    public int torqueF = 1;
    public int torqueR = 1;
    public byte[]   b = new byte[30];
    string bypass;
    int k;
    Socket s;
    int number;//identify the current veicle
    // Start is called before the first frame update
      public car()
     {
        s = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        Debug.Log("Establishing Connection to 192.168.0.0");//stream reader to obtain server data needed
        s.Connect("192.168.0.0", 8002);//connect to server
        Debug.Log("Connection established");
        k = s.Receive(b);//get data n of car and car data
        for (int i = 0; i < k; i++)
        {
            bypass += Convert.ToChar(b[i]);
        }
        Debug.Log(bypass);
        bypass = "";
        //set up all the variable of the cars 
     }
        // Update is called once per frame

    void Update()
    {
        currentspeed = FrontWheel.GetComponent<Rigidbody2D>().angularVelocity;
    }


    public void movefoward()
    {
        if (currentspeed > 100)
        {
            speedF += (float)(currentspeed * Brake);
            speedR += (float)(currentspeed * Brake);
        }
        else
        {
            if (speedR < Maxspeed)
            {
                speedF += torqueF;
                speedR += torqueR;
            }
        }
        BackWheel.GetComponent<Rigidbody2D>().AddTorque(-speedR * Time.fixedDeltaTime);
        BackWheel.GetComponent<Rigidbody2D>().AddTorque(-speedF * Time.fixedDeltaTime);
    }
    public void moveback()
    {

        if (currentspeed < -100)
        {
            speedF += (float)(currentspeed * Brake);
            speedR += (float)(currentspeed * Brake);
        }
        else
        {
            if (speedR > -Maxspeed)
            {
                speedF -= torqueF;
                speedR -= torqueR;
            }
        }
        BackWheel.GetComponent<Rigidbody2D>().AddTorque(-speedR * Time.fixedDeltaTime);
        BackWheel.GetComponent<Rigidbody2D>().AddTorque(-speedF * Time.fixedDeltaTime);
    }


}
