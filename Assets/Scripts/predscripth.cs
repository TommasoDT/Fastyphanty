using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predscripth : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject car;
    public int maxspeed;//velocità massima registrata
    public deathscripth dscript;
    public GameObject can;

    public GameObject BackWheel ;
    public GameObject FrontWheel ;
    private int time;
    private int pretime = 0;
    public int gaspicked=0;
    public float maxjump=0;
    private float ypos;
    public string[] frasiribaltamento = { "The end", "Uh il cielo non dovrebbe essere in alto?", "E adesso?", "Il carburante non è finito(solo perchè tu lo sappia)" };
    public string[] frasifuel = { "A secco", "Prossimamente macchina a pedali", "Se ci verso Il carburante dell'accendino riparte?", "E adesso come si torna indietro?", "Sto'ssenza nafta" };
    public string frase="menz";

    void Awake()
    {
        
        BackWheel = GameObject.Find("Wheel_Back");
        FrontWheel = GameObject.Find("Wheel_Front");
    }
    void Start()
    {
        maxspeed = 0;    //blocca tutto quello relativo al death overlay
        
        dscript.enabled = false;
        
        can.SetActive(false); 

        
    }

    // Script pre-morte
    void Update()
    {
        
        if (car.GetComponent<CarControls>().currentspeed.x > maxspeed) { maxspeed = (int)(car.GetComponent<CarControls>().currentspeed.x); };//rileva velocità massima;
        time = (int)Time.time;    

        if(!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8) && !FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8) &&!car.GetComponent<BoxCollider2D>().IsTouchingLayers(7 - 8))
        { if(maxjump<(car.GetComponent<Transform>().position.y)-ypos)
         {
                maxjump = car.GetComponent<Transform>().position.y - ypos;
            }       
        }
        else { ypos = car.GetComponent<Transform>().position.y; }
        
        
        
        
        
        
        //se il veicolo si trova a testa in giù fermo e il tempo scade
        if (!BackWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8) && !FrontWheel.GetComponent<CircleCollider2D>().IsTouchingLayers(7 - 8) && Mathf.Round(car.GetComponent<CarControls>().currentspeed.x) == 0 && Mathf.Round(car.GetComponent<CarControls>().currentspeed.y) == 0&& car.GetComponent<BoxCollider2D>().IsTouchingLayers(7 - 8)) 
        { if (time - pretime > 5)
            { frase = frasiribaltamento[Random.Range(0, 4)]; dscript.enabled = true; can.SetActive(true);  car.GetComponent<CarControls>().enabled = !car.GetComponent<CarControls>().enabled; this.enabled = !this.enabled; }
            
        }
        //se il veicolo finisce il carburante ed è fermo e il tempo scade
        else if (Mathf.Round(car.GetComponent<CarControls>().currentspeed.x) == 0 && Mathf.Round(car.GetComponent<CarControls>().currentspeed.y) == 0 && car.GetComponent<CarControls>().fuel == 0)
        {
            if (time - pretime > 5)
            { 
                frase = frasifuel[Random.Range(0, 4)]; dscript.enabled = true;
                car.GetComponent<Carsound>().outofuel = true;
                can.SetActive(true);  
                car.GetComponent<CarControls>().enabled = !car.GetComponent<CarControls>().enabled; this.enabled = !this.enabled; }
            
        }
        else//incrementa pretime 
        {
            pretime = (int)Time.time;
            
        }
       
    }
}
