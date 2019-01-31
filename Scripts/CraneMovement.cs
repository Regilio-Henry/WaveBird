using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{
    int number;
    int rotation;
    float speed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        number = Random.Range(0, 100);

        if (number == 0)
        {
            rotation = 1;
            speed = Random.Range(1, 10);
        }

        if (rotation == 1)
        {
            transform.Rotate(new Vector3(0, -speed, 0) * Time.deltaTime, Space.World);
        }

        if (number == 1)
        {
            rotation = 2;
            speed = Random.Range(1, 10);
        }

        if (rotation == 2)
        {
            transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime, Space.World);
        }

    }
}
