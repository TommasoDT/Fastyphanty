using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    // Start is called before the first frame update   
    public float speedmultiplier=40f;
    public float sizemultiplier=0.2f;
    public float despownoffset=10;
    public GameObject car;
    GameObject self;
    public Sprite[] sprites;
    void Start()
    {
        self = this.gameObject;
        self.gameObject.transform.localScale = new Vector3(self.transform.localScale.x*sizemultiplier * self.transform.position.y, self.gameObject.transform.localScale.y*sizemultiplier * self.transform.position.y, self.gameObject.transform.localScale.z);//dimensiona la nuvola in base alla altezza
        car = GameObject.Find("Car");
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        self.GetComponent<Rigidbody2D>().AddForce(new Vector2(-self.transform.position.y * speedmultiplier, 0));//aggiunge una velocità in base alla altezza
        if (car.transform.position.x > self.transform.position.x+despownoffset && !Tools.isInCameraXView(Camera.main, self.transform.position))
        {
            Destroy(self);
        }      
    }
}
