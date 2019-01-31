using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeVelocity : MonoBehaviour {
    public float Top;
    public float xpositions;
    public float ypositions;
    public float velocities;
    public float accelerations;
    public float baseHeight;
    public float Left;
    public float left;
    const float springconstant = 0.02f;
    const float damping = 0.04f;

    // Use this for initialization
    void Start () {
        baseHeight = Top;
        Left = left;
        ypositions = Top;
	}
	
    public void setPos(float baseheight)
    {
        float force = springconstant * (ypositions - baseheight) + velocities * damping;
        accelerations = -force;
        ypositions += velocities;
        velocities += accelerations;
    }

	// Update is called once per frame
	void Update ()
    {
        //transform.position = new Vector3(xpositions, ypositions);
	}
}
