using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    float yVelocity;
    float ground = -.75f;

    int jumpCount;

    KeyCode jump = KeyCode.Joystick1Button0;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Debug.Log(yVelocity);

        transform.Translate(new Vector3(0, yVelocity, 0) * Time.deltaTime, Space.World);

		if (Input.GetKeyDown(jump))
        {
            yVelocity = 10;
        }
        else if (transform.position.y > ground)
        {
            yVelocity -= .5f;
        }

        if (Input.GetKeyUp(jump) && yVelocity > 0)
        {
            yVelocity = 0;
        }

        if (transform.position.y < ground && !Input.GetKeyDown(jump))
        {
            transform.position = new Vector3(0, ground, 0);
            yVelocity = 0;
            jumpCount = 1;
        }
	}
}
