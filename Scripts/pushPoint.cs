using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushPoint : MonoBehaviour
{
    public GameObject sparkObject;
    GameObject[] wireCollection;
    int index = 0;
    float direction;
    Camera cam;
    public LayerMask playerMask;

	// Use this for initialization
	void Start ()
    {
        wireCollection = GameObject.Find("Test").GetComponent<test>().vertex;
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        print("touching");
        if (coll.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (coll.relativeVelocity.magnitude > 11f)
            {

                direction = coll.gameObject.GetComponent<Player_controler>().moveH;
                for (int i = 0; i < wireCollection.Length; i++)
                {
                    if (wireCollection[i] == gameObject)
                    {
                        index = i;
                    }
                }
                transform.Translate(0, coll.relativeVelocity.magnitude, 0);
                if (coll.gameObject.GetComponent<Player_controler>().sparkCount > 0)
                {
                    
                    GameObject spark = Instantiate(sparkObject);
                    spark.transform.position = transform.position;
                    spark.GetComponent<sparkController>().Init(index, direction);
                    coll.gameObject.GetComponent<Player_controler>().sparkCount--;
                }
                
                coll.transform.position = new Vector3(coll.transform.position.x,5);

                GetComponent<AudioSource>().Play();

            }
        }
    }
}
