using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathscripth : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToFollow;//automobile
    public Text Frase;
    public Text distanzap;//distanza percorsa
    public Text maxsp;//velocità massima rilevata
    public Text gaspic;//taniche di gas prese
    public Text jump;//salto più in alto
    private float distanza =0f;
    private float maxs =0f;
    private float carbrif=0f;
    private float salt = 0f;
    
    
    void Start()
    {
       
         


    }

    // Script che si attiva dopo la "morte"
    void Update()//metri auto=2.5 x => 3.75 m  ==> x = 1.5m
    {
         
        //transform.position = new Vector3(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y+2, 1);//segue la macchina
        //transform.localScale = new Vector3(Camera.main.orthographicSize * 0.7f, Camera.main.orthographicSize*0.7f, 1);//scala il death screen
        
            if(distanza < Mathf.Floor(ObjectToFollow.transform.position.x * 1.5f)) { distanza += 1f; }//incremento della distanza percorsa(animazione)
            if(maxs < (Mathf.Floor(GetComponent<predscripth>().maxspeed * 1.5f * 3.6f))) { maxs += 1f; }//incremento di maxs (animazione)
            if (carbrif < (GetComponent<predscripth>().gaspicked)) { carbrif += 1f; }//incremento di carbrif (animazione)
            if (salt < (Mathf.Floor(GetComponent<predscripth>().maxjump*1.5f))) { salt += 1f; }//incremento di carbrif (animazione)


        distanzap.text = "" + distanza + "m";//aggiona le scritte
            maxsp.text = ""+ maxs + "Km/h";
            gaspic.text =  ""+carbrif + " volte";
            jump.text = "" + salt+"m" ;
        Frase.text = GetComponent<predscripth>().frase;


    }

    public void exitme()
    {
        if(ObjectToFollow.transform.position.x>Tools.generalData.DistanzaRecord)
        {
        Tools.generalData.DistanzaRecord=ObjectToFollow.transform.position.x;
        Tools.SaveGeneralData();
        }
        else
        {
        Tools.SaveGeneralData();
        }
        SceneManager.LoadScene("Garage");
    }
}
