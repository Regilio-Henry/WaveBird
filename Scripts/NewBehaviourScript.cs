using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject start;
    public GameObject end;
    public float speed = .5f;
    Vector3 targetPos;
    // Use this for initialization
    void Start () {
        targetPos = end.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position == end.transform.position)
        {
            targetPos = start.transform.position;
        }

        if (transform.position == start.transform.position)
        {
            targetPos = end.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, .1f);
    }
}
