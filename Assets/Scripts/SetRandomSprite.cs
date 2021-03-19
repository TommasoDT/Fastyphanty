using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSprite : MonoBehaviour
{
    public List<Sprite> spriteList;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Count)];
        if(Random.Range(0, 2) == 0) //-1 behind car, 0 forward car
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        else
            GetComponent<SpriteRenderer>().sortingOrder = 1;

    }
}
