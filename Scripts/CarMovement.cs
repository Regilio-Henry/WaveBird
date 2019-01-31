using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{


	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(new Vector3(0, -5, 0) * Time.deltaTime, Space.Self);

        if (transform.position.x > 20)
        {
            transform.position = new Vector3(-15, -5.004f, -20);
        }

        if (transform.position.x < -20)
        {
            transform.position = new Vector3(15, -5.004f, -20);
        }
    }
}
