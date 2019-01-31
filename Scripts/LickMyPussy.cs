using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LickMyPussy : MonoBehaviour
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(new Vector3(-4, 0, 0) * Time.deltaTime, Space.World);
	}
}
