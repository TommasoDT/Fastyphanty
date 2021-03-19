using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public GameObject BackWheel;     
    public GameObject FrontWheel;
    public GameObject veicle;
    public GameObject dscript;
    public Vector2 currentspeed;
    public float torqueF = 0;// torque frontale
    public float torqueR = 0;//torque anteriore
    public int MaxtorquevariableR ;//torque massimo variabile
    public int MaxtorquevariableF ;//torque massimo variabile
    public int MaxtorqueairF ;//torque massimo per l'aria
    public int MaxtorqueairR ;//torque massimo per l'aria

    public float Brakefront ;//potenza freno anteriore
    public float Brakerear ;//potenza freno posteriore
    public float torqueFdv ;//valore che divide il max torque per ottenere il valore di accelerazione
    public float torqueRdv ;//valore che divide il max torque per ottenere il valore di accelerazione
    public float rotation_powa ;//potenza di rotazione
    public float fuel ;//fuel di partenza
    public int fuelvalue ;
    public int maxfuel ;
    public float frequency_front ;
    public float frequency_back ;
    public float dampening_front ;
    public float dampening_back ;
    public float height_front ;
    public float height_back ;
    public int divisoreconsumo ;//determina il consumo del carburante rapportato alla potenza , insomma l'efficenza , più alto = più efficiente
    public float[] cmass= new float[3] ;//massa del centro di gravità
    public int lvFsh ;
    public int lvRsh ;
    public int lvFgr ;
    public int lvRgr ;
    public int lvE ;
    public int lvT ;
    public int lvG ;
    public float gripR ;
    public float gripF ;



void Awake()
{
    dscript = GameObject.Find("death overlay");
    load();
    Debug.Log(Application.persistentDataPath + "/Car.dat");
}

public void carburante()//aggiunge carburante
{
    if (fuel <= maxfuel-fuelvalue)
                {fuel += fuelvalue; }
                else { fuel = maxfuel; }
}
    public void movefoward()
    {
        consumo();
        if (currentspeed[0] < -1)
        {
            BackWheel.GetComponent<Rigidbody2D>().angularDrag = Brakerear;//assegna la resistenza di rotazione mode "freno"
            FrontWheel.GetComponent<Rigidbody2D>().angularDrag = Brakefront;//assegna la resistenza di rotazione mode "freno"

        }        
        else
        {
            BackWheel.GetComponent<Rigidbody2D>().angularDrag = 0.05f;//assegna la resistenza di rotazione mode " no freno"
            FrontWheel.GetComponent<Rigidbody2D>().angularDrag = 0.05f;//assegna la resistenza di rotazione mode " no freno"

            if (torqueF < MaxtorquevariableF)//incrementa il torque se rispettate le condizioni
            {
                torqueF += MaxtorquevariableF/ torqueFdv;
               
            }
            if (torqueR < MaxtorquevariableR)//incrementa il torque se rispettate le condizioni
            {               
                torqueR += MaxtorquevariableR / torqueRdv;
            }

        }
        
        movecar();
        if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7-8)&& !FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8))
        { veicle.GetComponent<Rigidbody2D>().AddTorque(rotation_powa); }
        
    }
    public void moveback()
    {
        consumo();
        if (currentspeed[0] > 1)
        {
            BackWheel.GetComponent<Rigidbody2D>().angularDrag = Brakerear;//assegna la resistenza di rotazione mode "freno"
            FrontWheel.GetComponent<Rigidbody2D>().angularDrag = Brakefront;//assegna la resistenza di rotazione mode "freno"     

        }
        else
        {
            BackWheel.GetComponent<Rigidbody2D>().angularDrag = 0.05f;//assegna la resistenza di rotazione mode " no freno"
            FrontWheel.GetComponent<Rigidbody2D>().angularDrag = 0.05f;//assegna la resistenza di rotazione mode " no freno"

            if (torqueR > -MaxtorquevariableR)//incrementa il torque se rispettate le condizioni
            {              
                torqueR -= (MaxtorquevariableR / torqueRdv);
            }
            if (torqueF > -MaxtorquevariableF)//incrementa il torque se rispettate le condizioni
            {
                torqueF -= (MaxtorquevariableF / torqueFdv);               
            }

        }
        
        movecar();
        if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8) && !FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8))
        { veicle.GetComponent<Rigidbody2D>().AddTorque(-rotation_powa); }
    }
    public void movecar()
    {
       if(Time.time>veicle.GetComponent<Carsound>().now+2.5)
       {

       
        if (torqueR> MaxtorquevariableR) { torqueR = torqueR - torqueR / 10f; }
        if (torqueR < -MaxtorquevariableR) { torqueR = torqueR - torqueR / 10f; }//previene overshoot
        if (torqueF > MaxtorquevariableF) { torqueF = torqueF - torqueF / 10f; }
        if (torqueF < -MaxtorquevariableF) { torqueF = torqueF - torqueF / 10f; }


       
        

       
        FrontWheel.GetComponent<Rigidbody2D>().AddTorque(-torqueF / 10 * Time.fixedDeltaTime);//assegna la potenza alle gomme
        BackWheel.GetComponent<Rigidbody2D>().AddTorque(-torqueR / 10 * Time.fixedDeltaTime);//assegna la potenza alle gomme

       }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        veicle.GetComponent<Rigidbody2D>().centerOfMass = new Vector3(0f, -1f, 0f);
        currentspeed = veicle.GetComponent<Rigidbody2D>().velocity;//comandi
            //Input.GetKey for keyboards, LeftButtonButMOREPOWERFUL.pressed and RightButtonButMOREPOWERFUL.pressed is for android devices
            if ((Input.GetKey("left") || LeftButtonButMOREPOWERFUL.pressed) && fuel > 0)//vai indietro
            {
                if (!FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableF = MaxtorqueairF / 15; } else { MaxtorquevariableF = MaxtorqueairF / 2; }//smorza la potenza se la gomma è a mezzaria oppure NO
                if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableR = MaxtorqueairR / 15; } else { MaxtorquevariableR = MaxtorqueairR / 2; }//smorza la potenza se la gomma è a mezzaria oppure NO
                moveback();
            }
            if ((Input.GetKey("right") || RightButtonButMOREPOWERFUL.pressed) && fuel > 0)//vai avanti
            {
                if (!FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableF = MaxtorqueairF / 15; } else { MaxtorquevariableF = MaxtorqueairF; }//smorza la potenza se la gomma è a mezzaria oppure NO
                if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableR = MaxtorqueairR / 15; } else { MaxtorquevariableR = MaxtorqueairR; }//smorza la potenza se la gomma è a mezzaria oppure NO

                movefoward();
            }

            else//standby mode
            {
                consumo();
                if (!FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableF = MaxtorqueairF / 15; } else { MaxtorquevariableF = MaxtorqueairF; }//smorza la potenza se la gomma è a mezzaria oppure NO
                if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8)) { MaxtorquevariableR = MaxtorqueairR / 15; } else { MaxtorquevariableR = MaxtorqueairR; }//smorza la potenza se la gomma è a mezzaria oppure NO

                torqueF = torqueF - torqueF / 10;//riduzione torque progressiva
                torqueR = torqueR - torqueR / 10;//riduzione torque progressiva

                if (torqueF < 1 && torqueF > 0 || torqueF < 0 && torqueF > -1) { torqueF = 0; }//arrotonda a zero
                if (torqueR < 1 && torqueR > 0 || torqueR < 0 && torqueR > -1) { torqueR = 0; }
                movecar();
            }
        }
  void consumo()//riduce il carburante in base alla potenza + un tanto anche da fermo
  {
      if(fuel>0)
      {
          fuel=fuel-Mathf.Abs(((torqueR/divisoreconsumo)+0.1f));
      }
      if(fuel<0){fuel=0;}
    
  }

  public void editValue()
  {
      gameObject.GetComponents<WheelJoint2D>()[0].anchor=new Vector2( gameObject.GetComponents<WheelJoint2D>()[0].anchor.x,height_back);
      gameObject.GetComponents<WheelJoint2D>()[1].anchor=new Vector2( gameObject.GetComponents<WheelJoint2D>()[1].anchor.x,height_front);
      gameObject.GetComponents<WheelJoint2D>()[0].suspension= new JointSuspension2D() {angle=gameObject.GetComponents<WheelJoint2D>()[0].suspension.angle,frequency=frequency_back,dampingRatio=dampening_back};
      gameObject.GetComponents<WheelJoint2D>()[1].suspension= new JointSuspension2D() {angle=gameObject.GetComponents<WheelJoint2D>()[1].suspension.angle,frequency=frequency_front,dampingRatio=dampening_front};
      gameObject.transform.GetChild(2).GetComponent<CircleCollider2D>().sharedMaterial.friction=gripF;
      gameObject.transform.GetChild(1).GetComponent<CircleCollider2D>().sharedMaterial.friction=gripR;
  
  }


  public void load()
  {
        CarData data=Tools.LoadCar();      
        MaxtorquevariableR = data.MaxtorquevariableR;//torque massimo variabile
        MaxtorquevariableF = data.MaxtorquevariableF;//torque massimo variabile
        MaxtorqueairF = data.MaxtorqueairF;//torque massimo per l'aria
        MaxtorqueairR = data.MaxtorqueairR;//torque massimo per l'aria
        Brakefront = data.Brakefront;//potenza freno anteriore
        Brakerear = data.Brakerear;//potenza freno posteriore
        torqueFdv = data.torqueFdv;//valore che divide il max torque per ottenere il valore di accelerazione
        torqueRdv = data.torqueRdv;//valore che divide il max torque per ottenere il valore di accelerazione
        rotation_powa = data.rotation_powa;//potenza di rotazione
        fuel = data.fuel;//fuel di partenza
        fuelvalue = data.fuelvalue;
        maxfuel = data.maxfuel;
        divisoreconsumo = data.divisoreconsumo;//determina il consumo del databurante rapportato alla potenza , insomma l'efficenza , più alto = più efficiente
        cmass = data.cmass;//massa del centro di gravità
        frequency_front = data.frequency_front;
        frequency_back = data.frequency_back;
        dampening_front = data.dampening_front;
        dampening_back = data.dampening_back;
        height_front = data.height_front;
        height_back = data.height_back;
        editValue();
        Tools.saveCar(this);
  }
  public void save()
  {
      Tools.saveCar(this);
  }

}
