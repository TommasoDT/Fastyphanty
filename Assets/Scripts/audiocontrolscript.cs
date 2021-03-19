using System.Collections;
using System.Collections.Generic;
 using UnityEngine.UI;
using UnityEngine;

public class audiocontrolscript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button s;
    public Button m;
    public Sprite senabled;
    public Sprite sdisabled;
    public Sprite menabled;
    public Sprite mdisabled;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void sounds()
    {
      GameObject.Find("Car").GetComponent<Carsound>().sounds=!GameObject.Find("Car").GetComponent<Carsound>().sounds;
      if(GameObject.Find("Car").GetComponent<Carsound>().sounds==true){s.image.sprite=senabled;}else{s.image.sprite=sdisabled;}
       
    }
    public void musics()
    {
        Camera.main.GetComponent<CameraFollow>().audio=! Camera.main.GetComponent<CameraFollow>().audio;
        if(Camera.main.GetComponent<CameraFollow>().audio==true){m.image.sprite=menabled;}else{m.image.sprite=mdisabled;}
    }

}
