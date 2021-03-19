using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visumetri : MonoBehaviour
{
    // Start is called before the first frame update
    float maxmetri;
    float sott;
    public Text testo;
    void Start()
    {
         maxmetri=Mathf.Round(Tools.generalData.DistanzaRecord);
    }

    // Update is called once per frame
    void Update()
    {
        float pos = GameObject.Find("Car").transform.position.x;
        pos=Mathf.Round(pos);
        sott=maxmetri-pos;
        if(sott>0)
        {
            sott = Mathf.Round(sott*1.5f);
            testo.text = "-" + sott + " m";
        }
        else
        {
            pos = Mathf.Round(pos*1.5f);
            testo.text = "+" + pos + " m";
        }
    }
}
