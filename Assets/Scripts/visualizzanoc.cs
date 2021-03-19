using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visualizzanoc : MonoBehaviour
{
    // Start is called before the first frame update
    public Text noccioline;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        noccioline.text=""+Tools.generalData.MoneteRaccolte;
    }
}
