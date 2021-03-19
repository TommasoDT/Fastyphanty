using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Garage_script : MonoBehaviour
{
    GameObject car;
    public Button Ebutton;
    public int prezzoE = 100;
    public Button Sfbutton;
    public int prezzoSf = 40;
    public Button Srbutton;
    public int prezzoSr = 40;
    public Button Tbutton;
    public int prezzoT = 30;
    public Button Gbutton;
    public int prezzoG = 25;
    public Button Gfbutton ;
    public int prezzoGf = 20;
    public Button Grbutton;
    public int prezzoGr = 20;
    public Slider fH;
    public Slider rH;
    void Start()
    {
        car = GameObject.Find("Car"); 

        if(!(car.GetComponent<CarControls>().MaxtorqueairF<10000))                      //motore
        Ebutton.interactable=false;
        else
        Ebutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvE*1.5*prezzoE).ToString();

        if(!(car.GetComponent<CarControls>().frequency_front<4))                        //sospensione davanti
        Sfbutton.interactable=false;   
        else
        Sfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvFsh*1.5*prezzoE).ToString();  

        if(!(car.GetComponent<CarControls>().frequency_back<4))  //sospensione dietro
        {
             Srbutton.interactable=false;
             Srbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text="MAX";
        }                       
        
        else
        Srbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvRsh*1.5*prezzoE).ToString();    

        if(!(car.GetComponent<CarControls>().cmass[1]>-0.7))    
        {
         Gbutton.interactable=false;
         Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text="MAX";
        }                        //centro di gravità
              
        else
        Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvG*1.5*prezzoE).ToString();    

        if(!(car.GetComponent<CarControls>().maxfuel>10000)) //carburante 
        {
          Tbutton.interactable=false;  
          Tbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text="MAX";
        }                          
        
        else
        Tbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvT*1.5*prezzoE).ToString();   

        if(!(car.transform.GetChild(2).GetComponent<CircleCollider2D>().sharedMaterial.friction<13))    //grip davanti
        {
            Gfbutton.interactable=false;
            Gfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text="MAX";
        }
        
        else
        Gfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvFgr*1.5*prezzoE).ToString();   

        if(!(car.transform.GetChild(1).GetComponent<CircleCollider2D>().sharedMaterial.friction<13))     //grip dietro 
        {
          Grbutton.interactable=false;  
          Grbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text="MAX";
        }
        
        else
        Grbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text= Convert.ToInt32(car.GetComponent<CarControls>().lvRgr*1.5*prezzoE).ToString();   
        
        fH.value= car.GetComponent<CarControls>().height_front;
        rH.value= car.GetComponent<CarControls>().height_back;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!Tools.isInCameraXView(Camera.main,car.transform.position))
        {
            SceneManager.LoadScene("TestProceduralFloor");
        }
    }

    public void upgradeMotore()
    {

        if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvE*1.5*prezzoE))
        {
            if(car.GetComponent<CarControls>().MaxtorqueairF<10000)
            {
                car.GetComponent<CarControls>().MaxtorqueairF+=1000;
                car.GetComponent<CarControls>().MaxtorqueairR+=1000;                                                //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvE*1.5*prezzoE);//paga il costo
                car.GetComponent<CarControls>().lvE++;                                                            //alza il livello
                Ebutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvE*1.5*prezzoE).ToString(); //scrive il nuovo prezzo
            }
            else
            {
                Ebutton.interactable=false;                             //se sei al max lv non puoi più cliccarlo
            }
            Tools.saveCar(car.GetComponent<CarControls>());//aggiorna la macchina
        }
        else
        Ebutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 

    }
    public void FWheeljointUp()
    {
        Debug.Log(prezzoSf);
         if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvFsh*1.5*prezzoSf))
        {
            if(car.GetComponent<CarControls>().frequency_front<4)
            {
                car.GetComponent<CarControls>().frequency_front+=0.5F;                                                 //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvFsh*1.5*prezzoSf);//paga il costo
                car.GetComponent<CarControls>().lvFsh++;                                                             //alza il livello
                Sfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvFsh*1.5*prezzoSf).ToString();   //scrive il nuovo prezzo  
            }
            else
            {
                Sfbutton.interactable=false;
            }
            car.GetComponent<CarControls>().editValue();//applica modifiche
            Tools.saveCar(car.GetComponent<CarControls>());//aggiorna la macchina
        }
         else
         Sfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 

    }
     public void RWheeljointUp()
    {
        Debug.Log(prezzoSr);
        if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvRsh*1.5*prezzoSr))
        {
            if(car.GetComponent<CarControls>().frequency_back<4)
            {
                car.GetComponent<CarControls>().frequency_back+=0.5F;                                                  //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvRsh*1.5*prezzoSr);//paga il costo 
                car.GetComponent<CarControls>().lvRsh++;                                                             //alza il livello
                Srbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvRsh*1.5*prezzoSr).ToString();   //scrive il nuovo prezzo  

            }
            else
            {
                Srbutton.interactable=false;
            }
            car.GetComponent<CarControls>().editValue();
            Tools.saveCar(car.GetComponent<CarControls>());
        }
        else
        Srbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 
    }
     public void Gcenter()
    {
        if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvG*1.5*prezzoG))
        {
            if(car.GetComponent<CarControls>().cmass[1]>-0.7)
            {
                car.GetComponent<CarControls>().cmass[1]-=0.1F;                                                     //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvG*1.5*prezzoG);//paga il costo 
                car.GetComponent<CarControls>().lvG++;                                                            //alza il livello
                Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvG*1.5*prezzoG).ToString();   //scrive il nuovo prezzo          
            }
            else
            {
                Gbutton.interactable=false;
            }
            Tools.saveCar(car.GetComponent<CarControls>());

        }
        else
        Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 
    }
    public void Tanica()
    {
        if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvT*1.5*prezzoT))
        {
            if(car.GetComponent<CarControls>().maxfuel>10000)
            {
                car.GetComponent<CarControls>().maxfuel+=250;                                                       //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvT*1.5*prezzoT);//paga il costo 
                car.GetComponent<CarControls>().lvT++;                                                            //alza il livello
                Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvT*1.5*prezzoT).ToString();   //scrive il nuovo prezzo          
                 
            }
            else
            {
                Tbutton.interactable=false;
            }
            Tools.saveCar(car.GetComponent<CarControls>());
        }
        else
        Gbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 
    }

    public void Hfront(float h)
    {
        car.GetComponent<CarControls>().height_front=h;
        car.GetComponent<CarControls>().editValue();
        Tools.saveCar(car.GetComponent<CarControls>());
    }

    public void Hback(float h)
    {
        car.GetComponent<CarControls>().height_back=h;
        car.GetComponent<CarControls>().editValue();
        Tools.saveCar(car.GetComponent<CarControls>());

    }
    public void Grfront()
    {
        if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvFgr*1.5*prezzoGf))
        {
            if(car.transform.GetChild(2).GetComponent<CircleCollider2D>().sharedMaterial.friction<13)
            {
                car.GetComponent<CarControls>().gripF+=1;
                car.transform.GetChild(2).GetComponent<CircleCollider2D>().sharedMaterial.friction+=1;              //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvFgr*1.5*prezzoGf);//paga il costo 
                car.GetComponent<CarControls>().lvFgr++;                                                             //alza il livello
                Gfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvFgr*1.5*prezzoGf).ToString();   //scrive il nuovo prezzo
            }
            else
            {
                Gfbutton.interactable=false;
            }
        }
        else
        Gfbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 
    }
    public void Grrear()
    {  
         if(Tools.generalData.MoneteRaccolte>Convert.ToInt32(car.GetComponent<CarControls>().lvRgr*1.5*prezzoGr))
        {
            if( car.transform.GetChild(1).GetComponent<CircleCollider2D>().sharedMaterial.friction<13)
            {
                 car.GetComponent<CarControls>().gripR+=1;
                car.transform.GetChild(1).GetComponent<CircleCollider2D>().sharedMaterial.friction+=1;              //applica le modifiche
                Tools.generalData.MoneteRaccolte-=Convert.ToInt32(car.GetComponent<CarControls>().lvRgr*1.5*prezzoGr);//paga il costo 
                car.GetComponent<CarControls>().lvRgr++;                                                             //alza il livello
                Grbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToInt32(car.GetComponent<CarControls>().lvRgr*1.5*prezzoGr).ToString();   //scrive il nuovo prezzo
            }
            else
            {
                Grbutton.interactable=false;
            }
        }
        else
        Grbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color=Color.red;// non hai abbastanza soldi play sound 
    }
}
