using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class menuscript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target; //menu pausa
   public GameObject car;//auto
    public void onclick()//funzione resume
    {

        if(Time.timeScale==0){Time.timeScale=1;}
        else{Time.timeScale=0;}
        target.SetActive(!target.activeSelf);//attiva-disabilita menupausa
        car.GetComponent<AudioSource>().enabled=false;
        Camera.main.GetComponent<AudioSource>().enabled=false;
        //car.SetActive(!car.activeSelf);//attiva-disabilita auto



    }
    public void restart()//funzione restart
    {
        if(car.transform.position.x>Tools.generalData.DistanzaRecord)
        {
        Tools.generalData.DistanzaRecord=car.transform.position.x;
        Tools.SaveGeneralData();
        }
        else
        {
        Tools.SaveGeneralData();
        }
        SceneManager.LoadScene("TestProceduralFloor");//carica la scena
        GetComponent<Carsound>().now = Time.time;
        GetComponent<Carsound>().once = false;
       



    }

    public void goBackToMenu()
    {
        if(car.transform.position.x>Tools.generalData.DistanzaRecord)
        {
        Tools.generalData.DistanzaRecord=car.transform.position.x;
        Tools.SaveGeneralData();
        }
        else
        {
        Tools.SaveGeneralData();
        }
        SceneManager.LoadScene("Garage");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
